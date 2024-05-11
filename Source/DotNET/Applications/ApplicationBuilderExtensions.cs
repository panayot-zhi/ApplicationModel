// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications;
using Cratis.Execution;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Provides extension methods for the application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Use Cratis default setup.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/> to extend.</param>
    /// <returns><see cref="IApplicationBuilder"/> for continuation.</returns>
    public static IApplicationBuilder UseCratis(this IApplicationBuilder app)
    {
        Internals.ServiceProvider = app.ApplicationServices;

        app.UseMicrosoftIdentityPlatformIdentityResolver();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        if (RuntimeEnvironment.IsDevelopment)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger"));
        }

        app.UseDefaultLogging();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapIdentityProvider(app);
        });
        app.RunAsSinglePageApplication();

        return app;
    }
}
