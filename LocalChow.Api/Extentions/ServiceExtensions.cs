using LocalChow.Domain;
using LocalChow.Domain.Repository;
using LocalChow.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalChow.Api.Extentions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
        public static void ConfigureDbConnection(this IServiceCollection services, IConfiguration confugaration)
        {
            services.AddDbContext<LocalChowDbContext>(options => options.UseSqlServer(confugaration.GetConnectionString("DbConnection")));
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
    }
}
