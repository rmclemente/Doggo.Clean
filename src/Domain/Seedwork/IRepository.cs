namespace Domain.Seedwork
{
    /// <summary>
    /// Base Repository Interface exposing Unit of Work for context SaveChanges for Aggregate Root Entities. 
    /// </summary>
    /// <typeparam name="TEntity">IAggregateRoot Entites</typeparam>
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
