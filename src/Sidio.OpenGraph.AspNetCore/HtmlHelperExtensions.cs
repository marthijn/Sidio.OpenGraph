﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sidio.OpenGraph.AspNetCore;

/// <summary>
/// The HtmlHelperExtensions class provides extensions.
/// </summary>
public static class HtmlHelperExtensions
{
    /// <summary>
    /// Renders the Open Graph tags.
    /// </summary>
    /// <param name="htmlHelper">The HTML helper.</param>
    /// <returns>An <see cref="HtmlString"/>.</returns>
    public static HtmlString RenderOpenGraphTags(this IHtmlHelper htmlHelper)
    {
        if (htmlHelper.ViewContext.ViewData[Constants.ViewDataKey] is not OpenGraph openGraph)
        {
            return new HtmlString(string.Empty);
        }

        var html = openGraph.MetaTagsToHtml();
        return new HtmlString(html);
    }

    /// <summary>
    /// Gets the Open Graph prefix attribute value.
    /// </summary>
    /// <param name="htmlHelper">The HTML helper.</param>
    /// <returns>A <see cref="string"/>.</returns>
    public static string GetOpenGraphPrefixAttributeValue(this IHtmlHelper htmlHelper)
    {
        if (htmlHelper.ViewContext.ViewData[Constants.ViewDataKey] is not OpenGraph openGraph)
        {
            return string.Empty;
        }

        return openGraph.PrefixAttributeValue;
    }
}