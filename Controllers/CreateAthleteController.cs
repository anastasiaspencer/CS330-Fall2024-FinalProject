using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;

namespace CS330_Fall2024_FinalProject.Controllers;

public class CreateAthleteController : Controller
{
    private readonly AthleteApplicationService _applicationService;

    public CreateAthleteController(AthleteApplicationService applicationService)
    {
        _applicationService = applicationService;
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAthlete(CreateAthleteViewModel model)
    {
        if (ModelState.IsValid)
        {
        // Delegate to the application layer
        await _applicationService.AddAthleteAsync(model);

       
        return RedirectToAction("Index");
    }

    // for if validaiton fails
    return View(model);
    }
}
