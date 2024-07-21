# Sidio.OpenGraph
Sidio.OpenGraph is a .NET library for creating [OpenGraph](https://ogp.me/) tags. It suports
both .NET Standard 2.0 and .NET 8.

## Sidio.OpenGraph.AspNetCore
Sidio.OpenGraph.AspNetCore provides the same functionality as Sidio.OpenGraph, but it has some
helper functions to make it easier to use in an ASP.NET Core application. This library can be used in .NET 8
applications only.

# Installation
This package will be available on NuGet soon.

# Usage
## Builder pattern
The `OpenGraphBuilder` class is used to create an OpenGraph object:
```csharp
var builder = new OpenGraphBuilder();
builder.Add(new OpenGraphMetaTag("tag1", tag1));
builder.Add("tag2", tag2);
builder.Add("mycustomtag", customTag, new OpenGraphNamespace("ab", "https://example.com/ns#"));

var openGraph = builder.Build();
```

## Dependency injection
When using dependency injection, add the `IOpenGraphBuilderFactory` to the service collection by using the following
line of code. The advantage of using dependency injection is that an `ILogger` is injected in the builder 
which can provides some useful insights.
```csharp
services.AddOpenGraph();
```

Example:
```csharp
public class MyClass
{
    private readonly IOpenGraphBuilderFactory _factory;
    
    public MyClass(IOpenGraphBuilderFactory factory)
    {
        _factory = factory;
    }
    
    public void Example()
    {
        var  builder = _factory.CreateBuilder();
        builder.Add(new OpenGraphMetaTag("type", "example"));
        var openGraph = builder.Build();
    }
}    
```

## Asp.Net Core
Controller:
```csharp
public IActionResult Example1()
{
    this.SetOpenGraph(myOpenGraphObject);
    return View();
}

public IActionResult Example2()
{
    this.SetOpenGraph("Home", "website", "https://example.com/image.jpg", "https://example.com/");   
    return View();
}
```

View:
```cshtml
<html prefix="@Html.GetOpenGraphPrefixAttributeValue()">
<head>
  @Html.RenderOpenGraphTags()
</head>
```


# References
- [OpenGraph protocol](https://ogp.me/)
