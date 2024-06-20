using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cratis.Applications.Swagger;

/// <summary>
/// Extension methods for setting up Swagger for a Cratis application.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Adds the <see cref="ConceptSchemaFilter"/> <see cref="ISchemaFilter"/>.
    /// </summary>
    /// <param name="options">The <see cref="SwaggerGenOptions"/>.</param>
    public static void AddConcepts(this SwaggerGenOptions options) => options.SchemaFilter<ConceptSchemaFilter>();
}