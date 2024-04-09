using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Torc.BookLibrary.Configuration;

public class SwaggerService
{
    public static void Configure(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(configuration =>
        {
            configuration.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Library API",
                Version = "v1"
            });

            configuration.CustomSchemaIds(type => type.ToString());
        });
        services.AddSwaggerGenNewtonsoftSupport();
    }

    public static void ConfigureServices(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(configuration =>
        {
            configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
            configuration.RoutePrefix = "swagger";
            configuration.InjectStylesheet("/SwaggerUi.css");
        });
    }
}