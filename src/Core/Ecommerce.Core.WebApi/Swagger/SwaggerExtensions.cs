using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ecommerce.Core.WebApi.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services,
        string? title = null!,
        string? version = "v1",
        string? description = null!)
        => services.AddSwaggerGen(
            options =>
            {
                options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

                if (title is null && description is null)
                {
                    return;
                }

                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                    Description = description
                });
            });

    public static WebApplication UseSwaggerAndSwaggerUI(this WebApplication app,
        string routeTemplate = "api/{documentName}/swagger.json",
        string url = "/api/v1/swagger.json",
        string name = null!,
        string routePrefix = "api")
    {
        app.UseSwagger(opts =>
            {
                opts.RouteTemplate = routeTemplate;
            })
            .UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint(url, $"{name} API");
                opts.RoutePrefix = routePrefix;
            });

        return app;
    }
}
