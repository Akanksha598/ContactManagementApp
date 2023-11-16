using ContactManagementAPI.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementAPI.StartupExtensions
{
    public static class RegisterServicesStartup
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

            builder.Services.AddSwagger(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            }
            );

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return builder;
        }
    }
}
