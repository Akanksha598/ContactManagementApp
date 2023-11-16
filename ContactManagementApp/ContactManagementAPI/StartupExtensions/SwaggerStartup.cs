using Microsoft.OpenApi.Models;

namespace ContactManagementAPI.StartupExtensions;

public static class SwaggerStartup
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "Contact API",
                Description = "API to manage contacts",
                TermsOfService = new Uri("https://example.com/terms"),
            });

            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2.0",
                Title = "Contact API",
                Description = "API to manage contacts",
                TermsOfService = new Uri("https://example.com/terms"),
            });
        });

        return services;
    }
}
