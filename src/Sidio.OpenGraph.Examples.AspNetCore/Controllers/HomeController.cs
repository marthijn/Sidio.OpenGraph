using Microsoft.AspNetCore.Mvc;
using Sidio.OpenGraph.Examples.AspNetCore.Models;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Sidio.OpenGraph.AspNetCore;

namespace Sidio.OpenGraph.Examples.AspNetCore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        this.SetOpenGraph(
            "Home",
            "website",
            "https://example.com/image.jpg",
            "https://example.com/");

        return View();
    }

    [ExcludeFromCodeCoverage]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [ExcludeFromCodeCoverage]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}