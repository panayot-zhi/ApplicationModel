# Proxy Generation

To bridge the gap between the frontend and the backend, there is a tool for generating what we call proxies.
These are representations to be used in the frontend for artifacts in the backend. These are primarily grouped into 2
types; Commands & Queries.

The proxy generator runs as part of your build process leveraging the C# Roslyn compilers code generator extensibility point
to do this.

All you need to do is add a reference to the [Cratis.Applications.ProxyGenerator](https://www.nuget.org/packages/Cratis.Applications.ProxyGenerator/) NuGet
package and it will at compile time do the magic.

> Note: The projects that hold controllers should all have a reference to this package, since it is running as part of the
> compile steps.

The benefit of this is that you don't have to look at the Swagger API even to know what you have available, the code sits
there directly in the form of a generated proxy object

## Pre-requisites for the frontend

The proxy generator will generate artifacts that typically inherits and/or leverages things found in the base [`@cratis/applications`](https://www.npmjs.com/package/@cratis/applications)
NPM package. You will have to install this in your frontend project.

## Commands

Commands are the things you want to perform. These are represented as **HttpPost** operations on controllers. Any method arguments
are considered properties on the command. Complex types will have its properties added to the command directly as well.
Any of the parameters can be sourced using `[FromRoute]` or `[FromQuery]` and the generated proxy will generate the correct
route template based on whats in `[Route]` in combination with what is defined in `[HttpPost]`.

The command name is given my the method name of the action on the controller.

Take following controller with action in C#:

```csharp
[Route("/api/accounts/debit")]
public class DebitAccounts : Controller
{
    readonly IEventLog _eventLog;

    public DebitAccounts(IEventLog eventLog) => _eventLog = eventLog;

    [HttpPost]
    public Task OpenDebitAccount([FromBody] OpenDebitAccount create)
    {
        // Do things...
    }
}
```

From the controller you will get a generated command called `OpenDebitAccount`, it will take any route parameters, query string arguments and body
content and flatten it down as properties on the generated object. The generated type will inherit from the `Command` type found in`'@cratis/applications/commands`
You can read more on how to use commands [here](./commands.md).

As mentioned, you can also use things from the route, rather than from the body or you can combine them. All of it will be flattened down to
properties on the generated TypeScript command.

```csharp
[Route("/api/accounts/debit/{accountId}")]
public class DebitAccount : Controller
{
    readonly IEventLog _eventLog;

    public DebitAccount(IEventLog eventLog) => _eventLog = eventLog;

    [HttpPost("deposit/{amount}")]
    public Task DepositToAccount([FromRoute] AccountId accountId, [FromRoute] double amount)
    {
        // Do things
    }
}
```

## Queries

Queries are the data coming out. These are represents as **HttpGet** operations on controllers and returns either an enumerable
of a specific type or a single item of a type. These can have arguments which will also be part of the proxy objects. The generator will use the
method name as the query name, so remember to name these properly to get meaningful query objects for the frontend.

You can provide parameters to the queries as well. These can either be part of the route or as part of the query string.
(C#: `[FromRoute]` or `[FromQuery]`). The proxy generator will create a type that holds these and becomes compile-time
checked when using the query in the frontend.

Take the following controller action in C#:

```csharp
[HttpGet]
public IEnumerable<DebitAccount> AllAccounts()
{
    // Get data and return
}
```

> Note: Return types does not have to be an enumerable, it can also be a single item. However, when returning a collection
> of items - the return type should have a generic parameter of what the actual item type is. This is leveraged during
> the proxy generation.

The return type for the static method called `.use()` representing a React hook is of type `QueryResultWithState<>`.
This type contains additional information on whether or not the query is being performed or it is finished. This can be helpful for
knowing what to render and one could for instance enable a spinner when the property `isPerforming` is true.

### Observable Queries

The Cratis Application model comes with a fairly transparent way of doing queries that can be observed on the frontend for changes,
typically using WebSockets.

> Note: Head over to the [section for backend](../../backend/queries.md#observable-queries) to learn how to leverage this capability.

An observable query is generated in the same way, with the exception of it not having a method returned in the tuple to perform the
query explicitly. It is designed to be completely transparent and take advantage of the React rendering pipeline.

## Getting started

All you need is to reference the following **Cratis.ProxyGenerator.Build** package and configure the property for the output
folder within your **.csproj** file. Lets say you have a structure as below:

```xml
<PropertyGroup>
    <CratisProxyOutput>$(MSBuildThisFileDirectory)../Web</CratisProxyOutput>
</PropertyGroup>
```

The generator will maintain the folder structure from the source files while generating based on the namespaces of the files.

By default this means you will get every part of the namespace as sub folders in a hierarchy.
If you are working with multiple projects and have vertical slices that are consistently named a structure like below:

```shell
<Your Root Folder>
|
├-- Api
|   └-- MyFeature
├-- Domain
|   └-- MyFeature
├-- Events
|   └-- MyFeature
└-- Read
    └-- MyFeature
```

Assuming you have consistent namespacing for artifacts on every level, you'd get namespaces that are prefixed `Api.MyFeature`,
`Domain.MyFeature`, `Read.MyFeature` and so on. Since you might have artifacts that get generated from these different tiers,
for your frontend you might not care about what tier it belongs to but want a more unified model.

As an example, the **Domain** and **Read** projects could typically have ASP.NET Controllers within them representing commands and queries, respectively.
To get the **Domain** and **Read** part of removed in the final structure, the proxy generator supports a way of skipping segments
of the namespace while generating.

You achieve this by adding the following to your `.csproj` file that specifies the number of segments to skip:

```xml
<PropertyGroup>
    <CratisProxiesSegmentsToSkip>1</CratisProxiesSegmentsToSkip>
</PropertyGroup>
```
