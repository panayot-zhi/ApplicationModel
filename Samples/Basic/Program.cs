// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

// Force invariant culture for the Kernel
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseCratis();

var app = builder.Build();
app.UseRouting();
app.UseCratis();


app.MapGet("/", (request) =>
{
    request.User.Claims.ToList().ForEach(claim =>
    {
        Console.WriteLine($"{claim.Type} = {claim.Value}");
    });

    return Task.FromResult("Hello World!");
});

app.Run();
