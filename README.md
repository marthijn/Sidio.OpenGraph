# Sidio.OpenGraph
Sidio.OpenGraph is a .NET library for creating [OpenGraph](https://ogp.me/) tags. Both .NET Standard 2.0 and .NET 8+
are supported.

|| Sidio.OpenGraph| Sidio.OpenGraph.AspNetCore                                                                                                                |
|-|-|-------------------------------------------------------------------------------------------------------------------------------------------|
|*NuGet*| [![NuGet Version](https://img.shields.io/nuget/v/Sidio.OpenGraph)](https://www.nuget.org/packages/Sidio.OpenGraph/) | [![NuGet Version](https://img.shields.io/nuget/v/Sidio.OpenGraph.AspNetCore)](https://www.nuget.org/packages/Sidio.OpenGraph.AspNetCore/) |
|*Requirements*| .NET Standard 2.0 | .NET 8+, AspNetCore                                                                                                                       |

[![build](https://github.com/marthijn/Sidio.OpenGraph/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.OpenGraph/actions/workflows/build.yml)
[![Coverage Status](https://coveralls.io/repos/github/marthijn/Sidio.OpenGraph/badge.svg?branch=main)](https://coveralls.io/github/marthijn/Sidio.OpenGraph?branch=main)

## ASP .NET Core support
Sidio.OpenGraph.AspNetCore provides the same functionality as Sidio.OpenGraph, but it has some
helper functions to make it easier to use in an ASP.NET Core application. This library can be used in .NET 8 or higher
applications only.

# Installation
Get this package on NuGet:
- [Sidio.OpenGraph](https://www.nuget.org/packages/Sidio.OpenGraph/)
- [Sidio.OpenGraph.AspNetCore](https://www.nuget.org/packages/Sidio.OpenGraph.AspNetCore/)

# Usage
## Dependency injection
Dependency injection is recommended over creating instances of `OpenGraphBuilder` directly. The
`AddOpenGraph` extension method on `IServiceCollection` registers the following services:
- `IOpenGraphBuilderFactory`: factory for creating `OpenGraphBuilder` instances.
- `IObjectPoolService<OpenGraphBuilder>`: [object pool](https://en.wikipedia.org/wiki/Object_pool_pattern) for `OpenGraphBuilder` instances.
```csharp
services.AddOpenGraph();
```

Example:
```csharp
public class MyClass
{
    private readonly IOpenGraphBuilderFactory _factory;
    private readonly IObjectPoolService<OpenGraphBuilder> _pool;
    
    // use either the factory or the object pool. For demonstration purposes 
    // they are both included here.
    public MyClass(IOpenGraphBuilderFactory factory, IObjectPoolService<OpenGraphBuilder> pool)
    {
        _factory = factory;
        _pool = pool;
    }
    
    // with DI using the factory
    public void Example1()
    {
        var  builder = _factory.CreateBuilder();
        builder.Add(new OpenGraphMetaTag("type", "example"));
        builder.Add("tag2", tag2);
        var openGraph = builder.Build();
    }
    
    // with DI using an object pool
    public void Example2()
    {
        using var borrowedBuilder = _pool.Borrow();
        borrowedBuilder.Instance.Add(new OpenGraphMetaTag("type", "example"));
        borrowedBuilder.Instance.Add("tag2", tag2);
        var openGraph = builder.Instance.Build();
    }
    
    // without DI, not recommended
    public void Example3()
    {
        var builder = new OpenGraphBuilder();
        builder.Add(new OpenGraphMetaTag("tag1", tag1));
        builder.Add("tag2", tag2);
        
        // adding a custom tag with a custom namespace
        builder.Add("mycustomtag", customTag, new OpenGraphNamespace("ab", "https://example.com/ns#"));
        
        var openGraph = builder.Build();
    }
}    
```

## Asp.Net Core
Controller:
```csharp
// the builder pattern
public IActionResult Example1()
{
    this.SetOpenGraph(builder =>
        {
            builder.Add("title", title);
        });
    return View();
}

// the function with common tags
public IActionResult Example2()
{
    this.SetOpenGraph("Home", "website", "https://example.com/image.jpg", "https://example.com/");   
    return View();
}
```

Register tag helper (usually in `_ViewImports.cshtml`):
```cshtml
@addTagHelper *, Sidio.OpenGraph.AspNetCore
```

View (e.g. in `_Layout.cshtml`):
```cshtml
<!-- 1. add the prefix in the html tag -->
<html prefix="@Html.GetOpenGraphPrefixAttributeValue()">
<head>
  <!-- 2. render the open graph tags -->
  <open-graph />
</head>
```


# References
- [OpenGraph protocol](https://ogp.me/)
