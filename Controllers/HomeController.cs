using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;

namespace CS330_Fall2024_FinalProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
{
    // var userClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
    
    // // Iterate and print claims
    // foreach (var claim in userClaims)
    // {
    //     Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
    // }

    // // Alternatively, serialize to JSON for a single log statement
    // var claimsJson = System.Text.Json.JsonSerializer.Serialize(userClaims);
    // Console.WriteLine($"User Claims JSON: {claimsJson}");

    return View();
}




    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

