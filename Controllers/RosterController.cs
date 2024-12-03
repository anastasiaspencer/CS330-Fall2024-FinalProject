using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;

namespace CS330_Fall2024_FinalProject.Controllers;

public class RosterController : Controller
{
    private readonly AthleteApplicationService _applicationService;

    public RosterController(AthleteApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task<IActionResult> Index()
    {
        // Delegate to the application layer
        var athletes = await _applicationService.GetAllAthletesAsync();
        var rosterViewModel = new RosterViewModel(){
            Athletes = athletes.Select(a => new CreateAthleteViewModel
            {
                Name = a.Name,
                Number = a.Number,
            }).ToList()
        };

       
        return View(rosterViewModel);
  
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAthlete(string id){
        await _applicationService.DeleteAthleteAsync(id);
        return RedirectToAction("Index");
    }

}
