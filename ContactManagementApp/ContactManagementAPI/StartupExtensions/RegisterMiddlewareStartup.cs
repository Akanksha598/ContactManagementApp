namespace ContactManagementAPI.StartupExtensions;

public static class RegisterMiddlewareStartup
{
    public static WebApplication SetUpMiddleware(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactManagement_V1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "ContactManagement_V2");
            });
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }
}
