using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Sidio.ObjectPool;

namespace Sidio.OpenGraph.AspNetCore;

/// <summary>
/// Tag helper for rendering Open Graph meta tags.
/// </summary>
[HtmlTargetElement("open-graph", TagStructure = TagStructure.WithoutEndTag)]
public sealed class OpenGraphTagHelper : TagHelper
{
    private readonly IObjectPoolService<StringBuilder> _stringBuilderPoolService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphTagHelper"/> class.
    /// </summary>
    /// <param name="stringBuilderPoolService">The string builder pool service.</param>
    public OpenGraphTagHelper(IObjectPoolService<StringBuilder> stringBuilderPoolService)
    {
        _stringBuilderPoolService = stringBuilderPoolService;
    }

    /// <summary>
    /// Gets the view context.
    /// </summary>
    [ViewContext]
    [HtmlAttributeNotBound]
    public required ViewContext ViewContext { get; init; }

    /// <inheritdoc />
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        if (ViewContext.ViewData[Constants.ViewDataKey] is OpenGraph openGraph)
        {
            var html = openGraph.MetaTagsToHtml(_stringBuilderPoolService.Pool);
            output.Content.SetHtmlContent(html);
        }
    }
}