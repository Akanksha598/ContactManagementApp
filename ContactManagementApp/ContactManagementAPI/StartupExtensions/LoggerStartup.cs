using Serilog;

namespace ContactManagementAPI.StartupExtensions;

public static class LoggerStartup
{
    public static WebApplicationBuilder RegisterLogger(this WebApplicationBuilder services)
    {
        var logger = new LoggerConfiguration()
     .ReadFrom.Configuration(services.Configuration)
     .Enrich.FromLogContext()
     .CreateLogger();

        services.Logging.ClearProviders();
        services.Logging.AddSerilog(logger);

        return services;
    }
}
