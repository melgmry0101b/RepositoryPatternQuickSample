// LICENSE: The Unlicense

using System.Threading.Tasks;

namespace RepositoryPatternQuickSample.Contracts
{
    public interface IRepository<TModel, TUnitOfWork>
    {
        TUnitOfWork UnitOfWork { get; }

        Task SaveChangesAsync();
    }
}
