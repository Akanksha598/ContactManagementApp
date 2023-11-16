using System.Configuration;

namespace ContactManagementAPI.Extensions;

public static class ConfigurationExtensions
{
    public static string GetConnectionStringOrThrowException(this IConfiguration configuration, string connectionName)
    {
        var connectionString = configuration.GetConnectionString(connectionName);

        return connectionString ?? throw new ConfigurationErrorsException($"Unable to get connection string for {connectionName}");
    }
}
