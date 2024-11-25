using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;

namespace CS330_Fall2024_FinalProject.Controllers;

public class SnowReport : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
