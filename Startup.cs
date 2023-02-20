// LICENSE: The Unlicense

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPatternQuickSample.Contexts;
using RepositoryPatternQuickSample.Contracts;
using RepositoryPatternQuickSample.Repositories;

namespace RepositoryPatternQuickSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PetsDbContext>(options => options.UseInMemoryDatabase("PetsDbContext"));

            services.AddScoped<ICatsRepository, CatsRepository>();

            services.AddControllers();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            // Swagger is for API Endpoints exploration and quick usage.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
