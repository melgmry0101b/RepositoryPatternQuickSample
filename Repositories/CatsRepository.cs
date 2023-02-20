// LICENSE: The Unlicense

using Microsoft.EntityFrameworkCore;
using RepositoryPatternQuickSample.Contexts;
using RepositoryPatternQuickSample.Contracts;
using RepositoryPatternQuickSample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPatternQuickSample.Repositories
{
    public class CatsRepository : ICatsRepository
    {
        public PetsDbContext UnitOfWork { get; }

        public CatsRepository(PetsDbContext petsDbContext)
        {
            UnitOfWork = petsDbContext;
        }

        public async Task SaveChangesAsync()
        {
            await UnitOfWork.SaveChangesAsync();
        }

        public Task<Cat> CreateCatAsync(string name, string color)
        {
            return Task.FromResult(UnitOfWork.Cats.Add(new Cat { Name = name, Color = color }).Entity);
        }

        public async Task<Cat?> GetCatAsync(int id)
        {
            return await UnitOfWork.Cats.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cat>> GetAllCatsAsync()
        {
            return await UnitOfWork.Cats.ToListAsync();
        }

        public async Task<Cat?> DeleteCatAsync(int id)
        {
            Cat? cat = await UnitOfWork.Cats.FirstOrDefaultAsync(c => c.Id == id);
            if (cat is null)
            {
                return null; // Cat not found
            }

            return UnitOfWork.Remove(cat).Entity;
        }
    }
}
