# ForEvolve.Testing

There is always something unexpected that happens or something that requires a complex mock setup or whatnot when testing web applications. This project aim at regrouping as many of those solutions as possible, ready to use, as well as multiple small utilities.

If your specific use-case is not covered, you can always contribute it or open an issue.

[![Build Status - releases/1.0.0](https://forevolve.visualstudio.com/ForEvolve-Framework/_apis/build/status/ForEvolve.Testing?branchName=releases/1.0.0)](https://forevolve.visualstudio.com/ForEvolve-Framework/_build/latest?definitionId=42&branchName=releases/1.0.0)
[![Build Status - master](https://forevolve.visualstudio.com/ForEvolve-Framework/_apis/build/status/ForEvolve.Testing?branchName=master)](https://forevolve.visualstudio.com/ForEvolve-Framework/_build/latest?definitionId=42&branchName=master)

## Versioning

The packages should follows _sementic versioning_.

## Hot to use

### NuGet

All packages are available on [https://www.nuget.org/profiles/ForEvolve](https://www.nuget.org/profiles/ForEvolve).

To load all testing packages, reference the `ForEvolve.Testing` package:

```PowerShell
Install-Package ForEvolve.Testing
```

or

```PowerShell
dotnet add package ForEvolve.Testing
```

#### Individual packages (App)

To load individual packages, you can:

```PowerShell
Install-Package ForEvolve.Testing.Core
Install-Package ForEvolve.Testing.AspNetCore
```

or

```PowerShell
dotnet add package ForEvolve.Testing.Core
dotnet add package ForEvolve.Testing.AspNetCore
```

### Prerelease MyGet

For the pre-release packages, use the ForEvolve [NuGet V3 feed URL](https://www.myget.org/F/forevolve/api/v3/index.json) packages source. See the [Table of content](https://github.com/ForEvolve/Toc) project for more info.

### Pre-release build number

Pre-release builds number are autoincremented.

## Content

The repository is divided into multiple projects, loadable individually, and assembled as one in `ForEvolve.Testing`.

### ForEvolve.Testing

This project load all of the other projects to create a NuGet package to rule them all! You can load this package or any other individually.

### ForEvolve.Testing.Core

This project contains multiple test helpers.

#### General assertion extensions

-   An extension on `KeyValuePair<string, object>` to `keyValue.AssertEqual("some key", "some object")` that assert the equalities.
-   An extension on `Stream` to `stream.ShouldEqual("some string")` that read the `Stream` then `Assert.Equal` its value against the specified `expectedResult`.
-

#### `IServiceCollection` extensions

-   `services.AddSingletonMock<TService>()` or `services.AddSingletonMock<TService>(mock => { /*some setup code*/ })` that add a mock with a singleton lifetime to the `IServiceCollection`.
-   `services.AddScopedMock<TService>()` or `services.AddScopedMock<TService>(mock => { /*some setup code*/ })` that add a mock with a scoped lifetime to the `IServiceCollection`.
-   `services.AddTransientMock<TService>()` or `services.AddTransientMock<TService>(mock => { /*some setup code*/ })` that add a mock with a transient lifetime to the `IServiceCollection`.

#### `IServiceProvider` assertion extensions

-   `serviceProvider.AssertServiceExists<TInterface>()` that `GetRequiredService<TInterface>()`, throwing an exception if the dependency does not exist.
-   `serviceProvider.AssertServiceImplementationExists<TInterface, TImplementation>()` that `GetRequiredService<TInterface>()`, throwing an exception if the dependency does not exist, then validate that its implementation is of type `TImplementation`.
-   `serviceProvider.AssertServicesImplementationExists<TInterface, TImplementation>()` that `GetServices<TInterface>()`, make sure there is at least one implementation of type `TImplementation`. Throw a `TrueException` when no binding are of type `TImplementation`.
-   Multiple other useful method to test the content of the `IServiceCollection` to ensure that dependencies are registered against the DI Container, under the expected scope. Moreover, those methods are chainable. For example, we could:
    ```csharp
    services
        .AssertSingletonServiceExists<SomeService>()
        .AssertSingletonServiceImplementationExists<ISomeOtherService, DefaultSomeOtherService>()
        .AssertScopedServiceExists<SomeScopedService>()
        ;
    ```

### ForEvolve.Testing.AspNetCore

This project contains some Asp.Net Core test utilities like:

-   `IdentityHelper` that creates the plumbing to mock `SignInManager<TUser>` and `UserManager<TUser>`. Multiple authentication scenarios are supported.
-   `HttpContextHelper` that creates the plumbing to mock `HttpContext`, `HttpRequest`, and `HttpResponse`.
-   `MvcContextHelper` at creates the plumbing to mock `IUrlHelper` and `ActionContext`; inclusing support for `RouteData`, `ActionDescriptor`, and `ModelStateDictionary`.
-   An extension method on `HttpResponse` to `httpResponse.BodyShouldEqual("Some value")`
-   An extension on `WebApplicationFactory<TEntryPoint>` to `webApplicationFactory.FindServiceCollection()` that return the private `webApplicationFactory.Server.Host._applicationServiceCollection` property.
-   An extensnio on `IServiceCollection` to `services.RemoveFilter<TFilter>()` that remove the specified filter from `MvcOptions`. The method rely on `PostConfigure<MvcOptions>(...)`.

## History

I started this project to share test utilities between projects and it now includes multiple testing utilities for .Net Core and Asp.Net Core.

The initial code comes from the `ForEvolve.XUnit` project, see [ForEvolve-Framework](https://github.com/ForEvolve/ForEvolve-Framework).

## The future

Here are a few things that I'd like to add:

-   An in-memory test server that replies configurable predefined values. This could become an in-memory mock API to test clients or other APIs.
-   An easy way to connect an in-memory test host to another in-memory Identity Server 4 to fully test authentication.

There is much stuff that I want to improve or add to this library in the future, and since I use it myself in my projects, I will probably do if the time allows.

## How to contribute?

If you would like to contribute to the project, first, thank you for your interest and please read [Contributing to ForEvolve open source projects](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/CONTRIBUTING.md) for more information.

### Contributor Covenant Code of Conduct

Also, please read the [Contributor Covenant Code of Conduct](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/CODE_OF_CONDUCT.md) that applies to all ForEvolve repositories.
