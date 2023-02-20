// LICENSE: The Unlicense

using Microsoft.EntityFrameworkCore;
using RepositoryPatternQuickSample.Models;

namespace RepositoryPatternQuickSample.Contexts
{
    public class PetsDbContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; } = default!;

        public PetsDbContext(DbContextOptions<PetsDbContext> options) : base(options) { }
    }
}
