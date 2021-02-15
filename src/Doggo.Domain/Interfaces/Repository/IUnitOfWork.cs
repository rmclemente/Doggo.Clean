using System.Threading.Tasks;

namespace Doggo.Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface to apply Unit of Work name pattern, to call context SaveChanges
    /// </summary>
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
