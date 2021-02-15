using Doggo.Domain.Entities;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.Data.Mappings.Parametrizacao;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Doggo.Infra.Data.Context
{
    public class DoggoCleanContext : DbContext, IUnitOfWork
    {
        public DoggoCleanContext(DbContextOptions<DoggoCleanContext> options) : base(options)
        {
        }

        //DbSet Mappings
        public virtual DbSet<Breed> Breed { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Set undefined string properties to varchar(100);
            var entityStringProperties = modelBuilder.Model
                                            .GetEntityTypes()
                                            .SelectMany(e => e.GetProperties()
                                            .Where(p => p.ClrType == typeof(string)));

            foreach (var property in entityStringProperties)
                property.SetColumnType("varchar(100)");

            //Delete behavior
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            //Ignore mapping types
            //modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();
            //modelBuilder.Ignore<PropertyInfo>();

            //Auto mapping of map classes
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);

            //Mapping Classes
            modelBuilder.ApplyConfiguration(new BreedMapping());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedBy") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    entry.Property("CreatedBy").CurrentValue = "Doggo";
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("CreatedBy").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedBy") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("UpdatedAt").IsModified = false;
                    entry.Property("UpdatedBy").IsModified = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    entry.Property("UpdatedBy").CurrentValue = "Doggo";
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
