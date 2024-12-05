using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;

// not same as SkiStats stuff but should prob pull in SkiStats stuff in here

namespace CS330_Fall2024_FinalProject.Controllers;


public class StatsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
