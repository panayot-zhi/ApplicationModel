// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.Json.Nodes;
using Cratis.Applications.MongoDB;
using Cratis.Json;
using Cratis.MongoDB;
using Main;
using MongoDB.Driver;
using Orleans.Serialization;
using Orleans.Serialization.Cloning;
using Orleans.Serialization.Serializers;

// Force invariant culture for the Backend
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication
    .CreateBuilder(args)
    .UseApplicationModel();

// Todo: This should be part of the "Use application model" extension method, with overrides
builder.Services.AddDefaultModelNameConvention();
builder.Host.UseMongoDB();

var mongoClient = new MongoClient("mongodb://localhost:27017");
builder.Services.AddSingleton(mongoClient);
builder.Services.AddSingleton(mongoClient.GetDatabase("eCommerce"));
builder.Services.AddTransient(typeof(IMongoCollection<>), typeof(MongoCollectionAdapter<>));

builder.UseOrleans(_ => _
    .ConfigureServices(services =>
    {
        services.AddConceptSerializer();
        services.AddSerializer(serializerBuilder => serializerBuilder.AddJsonSerializer(
            _ =>
                _ == typeof(JsonObject) ||
                (_.Namespace?.StartsWith("Read") ?? false) ||
                (_.Namespace?.StartsWith("Concepts") ?? false),
            Globals.JsonSerializerOptions));
    })
    .UseLocalhostClustering()
    .AddMongoDBStorageAsDefault(options =>
    {
        options.ConnectionString = "mongodb://localhost:27017";
        options.Database = "eCommerce";
    })
    .UseDashboard(options =>
    {
        options.Host = "*";
        options.Port = 8081;
        options.HostSelf = true;
    }));

var app = builder.Build();
app.UseWebSockets();
app.UseRouting();
app.UseApplicationModel();

await app.RunAsync();
