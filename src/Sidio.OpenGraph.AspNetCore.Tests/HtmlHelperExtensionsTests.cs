using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Sidio.OpenGraph.AspNetCore.Tests;

public sealed class HtmlHelperExtensionsTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void RenderOpenGraphTags_WhenViewDataIsSet_ReturnsHtmlString()
    {
        // arrange
        var propertyName = _fixture.Create<string>();
        var content = _fixture.Create<string>();
        var builder = new OpenGraphBuilder();
        builder.Add(propertyName, content, new OpenGraphNamespace(_fixture.Create<string>(), _fixture.Create<string>()));
        var openGraph = builder.Build();

        var htmlHelper = new Mock<IHtmlHelper>();
        var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) {{Constants.ViewDataKey, openGraph}};
        var viewContext = new ViewContext
        {
            ViewData = viewDataDictionary,
        };

        htmlHelper.SetupGet(x => x.ViewContext).Returns(viewContext);

        // act
        var result = htmlHelper.Object.RenderOpenGraphTags();

        // assert
        result.Should().NotBeNull();
        result.Value.Should().Contain(propertyName).And.Contain(content);
    }

    [Fact]
    public void GetOpenGraphPrefixAttributeValue_WithNamespace_ReturnsPrefixValue()
    {
        // arrange
        var prefix = _fixture.Create<string>();
        var schema = _fixture.Create<string>();
        var builder = new OpenGraphBuilder();
        builder.Add(_fixture.Create<string>(), _fixture.Create<string>(), new OpenGraphNamespace(prefix, schema));
        var openGraph = builder.Build();

        var htmlHelper = new Mock<IHtmlHelper>();
        var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) {{Constants.ViewDataKey, openGraph}};
        var viewContext = new ViewContext
        {
            ViewData = viewDataDictionary,
        };

        htmlHelper.SetupGet(x => x.ViewContext).Returns(viewContext);

        // act
        var result = htmlHelper.Object.GetOpenGraphPrefixAttributeValue();

        // assert
        result.Should().NotBeNull();
        result.Should().Be($"{prefix}: {schema}");
    }
}