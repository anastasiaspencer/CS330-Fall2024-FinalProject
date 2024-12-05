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
        var athletes = await _applicationService.GetAllAthletesAsync();
        var rosterViewModel = new RosterViewModel()
        {
            Athletes = athletes.Select(a => new CreateAthleteViewModel
            {
                Name = a.Name,
                // Number = a.Number,
              //  Id = a.Id,
                Birthday = a.Birthday,
                SkiLevel = a.SkiLevel,
                BestTime = a.Stats?.BestTime ?? 0,
                TopSpeed = a.Stats?.TopSpeed ?? 0,
                BestDistance = a.Stats?.BestDistance ?? 0,
                VerticalDrop = a.Stats?.VerticalDrop ?? 0,
                Ranking = a.Stats?.Ranking ?? 0 

            }).ToList()
        };


        return View(rosterViewModel);

    }
    

    [HttpPost]
    public async Task<IActionResult> DeleteAthlete(int id)
    {
        await _applicationService.DeleteAthleteAsync(id);
        return RedirectToAction("Index");
    }

}
