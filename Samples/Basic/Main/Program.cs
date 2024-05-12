// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

// Force invariant culture for the Kernel
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);
builder.UseOrleans(_ => _
    .UseLocalhostClustering()
    .UseDashboard(options =>
    {
        options.Host = "*";
        options.Port = 8081;
        options.HostSelf = true;
    }));
var app = builder.Build();

await app.RunAsync();
