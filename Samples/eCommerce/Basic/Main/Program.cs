// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Main;
using MongoDB.Driver;
using Orleans.Serialization;
using Orleans.Serialization.Cloning;
using Orleans.Serialization.Serializers;

// Force invariant culture for the Kernel
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication
    .CreateBuilder(args)
    .UseApplicationModel();

builder.UseOrleans(_ => _
    .ConfigureServices(services => services.AddConceptSerializer())
    .UseLocalhostClustering()
    .UseDashboard(options =>
    {
        options.Host = "*";
        options.Port = 8081;
        options.HostSelf = true;
    }));

var app = builder.Build();
app.UseRouting();
app.UseApplicationModel();

await app.RunAsync();
