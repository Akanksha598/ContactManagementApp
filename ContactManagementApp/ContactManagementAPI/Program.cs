using ContactManagementAPI.StartupExtensions;

WebApplication
    .CreateBuilder(args)
    .RegisterLogger()
    .RegisterServices()
    .RegisterRepositories()
    .Build()
    .SetUpMiddleware()
    .Run();