using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;

namespace CS330_Fall2024_FinalProject.Controllers;

public class ScheduleController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
