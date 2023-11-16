namespace ContactManagementAPI.StartupExtensions;

public static class RepositoriesStartup
{
    public static WebApplicationBuilder RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterCommonRepositories(builder.Configuration);

        return builder;
    }
}
