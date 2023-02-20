// LICENSE: The Unlicense

using RepositoryPatternQuickSample.Contexts;
using RepositoryPatternQuickSample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPatternQuickSample.Contracts
{
    public interface ICatsRepository : IRepository<Cat, PetsDbContext>
    {
        Task<Cat> CreateCatAsync(string name, string color);
        Task<Cat?> DeleteCatAsync(int id);
        Task<IEnumerable<Cat>> GetAllCatsAsync();
        Task<Cat?> GetCatAsync(int id);
    }
}
