namespace Domain.Seedwork
{
    /// <summary>
    /// Interface to expose context SaveChanges, named in reference to Unit of Work pattern implemented by Entity Framework Core.
    /// </summary>
    public interface IUnitOfWork
    {
        Task<bool> Commit(CancellationToken cancellationToken = default);
    }
}