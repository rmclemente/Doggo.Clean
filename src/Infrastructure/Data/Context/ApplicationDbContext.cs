using Domain.Core.SeedWork;
using Domain.Entities;
using Domain.Seedwork;
using Infrastructure.Data.EntitiesConfiguration;
using Infrastructure.Data.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        #region DbSet Configuration
        public virtual DbSet<Breed> Breeds { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ModelBuilder Configuration
            modelBuilder.HasDefaultSchema("doggo");
            modelBuilder.SetStringsAsNonUnicodeGlobally();
            modelBuilder.SetStringPropertyMaxLengthGlobally(nameof(AuditableEntity.CreatedBy), 150);
            modelBuilder.SetStringPropertyMaxLengthGlobally(nameof(AuditableEntity.LastUpdateBy), 150);
            #endregion

            #region EntityType Configurations
            modelBuilder.ApplyConfiguration(new BreedConfiguration());
            #endregion

            #region Ignore mapping types
            modelBuilder.Ignore<Event>();
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().BaseType == typeof(AuditableEntity)))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameof(AuditableEntity.CreatedAt)).CurrentValue = DateTimeOffset.UtcNow;
                    entry.Property(nameof(AuditableEntity.CreatedBy)).CurrentValue = "admin";
                    entry.Property(nameof(AuditableEntity.LastUpdateAt)).IsModified = false;
                    entry.Property(nameof(AuditableEntity.LastUpdateBy)).IsModified = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(nameof(AuditableEntity.CreatedAt)).IsModified = false;
                    entry.Property(nameof(AuditableEntity.CreatedBy)).IsModified = false;
                    entry.Property(nameof(AuditableEntity.LastUpdateAt)).CurrentValue = DateTimeOffset.UtcNow;
                    entry.Property(nameof(AuditableEntity.LastUpdateBy)).CurrentValue = "admin";
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken) > 0;
            if (result) await _mediator.DispatchDomainEvents(this, cancellationToken);
            return result;
        }

        #region Transaction Handle
        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            if (HasActiveTransaction)
                return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            return _currentTransaction;
        }

        public async Task CommitTransaction(IDbContextTransaction transaction)
        {
            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await Commit();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction is not null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction is not null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        #endregion
    }
}
