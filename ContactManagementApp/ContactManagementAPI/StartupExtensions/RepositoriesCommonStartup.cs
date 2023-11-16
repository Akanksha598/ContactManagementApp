using ContactManagementAPI.Data;
using ContactManagementAPI.Extensions;
using ContactManagementAPI.Mapping;
using ContactManagementAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementAPI.StartupExtensions
{
    public static class RepositoriesCommonStartup
    {
        public static IServiceCollection RegisterCommonRepositories(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionStringOrThrowException("DefaultSQLConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            );

            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddAutoMapper(typeof(MappingConfig));


            return services;
        }
    }
}
