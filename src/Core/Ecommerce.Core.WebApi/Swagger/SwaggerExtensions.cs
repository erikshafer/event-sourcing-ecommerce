using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Core.WebApi.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenWithCustomSchemaIds(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
            options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
        });
}
