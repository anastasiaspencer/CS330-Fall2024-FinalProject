using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;

namespace CS330_Fall2024_FinalProject.Controllers;

public class ScheduleController : Controller
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    public async Task<IActionResult> Index()
    {
        var schedules = await _scheduleService.GetAllAsync();

        return View(schedules);
    }

    [HttpGet]
    //[Authorize(Roles = "Coach,Manager")]
    //OR?
    //[Authorize(Policy = "RequireCoach")]
    //[Authorize(Policy = "RequireManager")]
    public IActionResult Create()
    {
        var schedule = new Schedule
        {
            Date = DateTime.Now
        };
        return View();
    }

    [HttpPost]
    //[Authorize(Roles = "Coach,Manager")]
    //OR?
    //[Authorize(Policy = "RequireCoach")]
    //[Authorize(Policy = "RequireManager")]
    public async Task<IActionResult> Create(Schedule schedule, string Time)
    {
        if (ModelState.IsValid)
        {
            if (DateTime.TryParse(schedule.Date.ToString("yyyy-MM-dd") + " " + Time, out DateTime combinedDateTime))
            {
                schedule.Date = combinedDateTime;
                await _scheduleService.AddAsync(schedule);
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid date or time format.");
        }
        return View(schedule);
    }
}
