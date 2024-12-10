using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;
using CS330_Fall2024_FinalProject.Repositories;
using Microsoft.EntityFrameworkCore;
using CS330_Fall2024_FinalProject.Data;

// not same as SkiStats stuff but should prob pull in SkiStats stuff in here

namespace CS330_Fall2024_FinalProject.Controllers;


public class StatsController : Controller
{

    private readonly SkiStatsRepository _skiStatsRepository;

    // private readonly ApplicationDbContext? _context;

    public StatsController(SkiStatsRepository skiStatsRepository)
    {
        _skiStatsRepository = skiStatsRepository;
    }
    public IActionResult Index()
    {

        var records = new List<SkiStatsViewModel>
        {
            new SkiStatsViewModel
            {
                StatName = "Best Time",
                Value = _skiStatsRepository.GetBestTime().Value,
                AthleteName = _skiStatsRepository.GetBestTime().AthleteName
            },
            new SkiStatsViewModel
            {
                StatName = "Top Speed",
                Value = _skiStatsRepository.GetTopSpeed().Value,
                AthleteName = _skiStatsRepository.GetTopSpeed().AthleteName
            },
            new SkiStatsViewModel
            {
                StatName = "Best Distance",
                Value = _skiStatsRepository.GetBestDistance().Value,
                AthleteName = _skiStatsRepository.GetBestDistance().AthleteName
            },
            new SkiStatsViewModel
            {
                StatName = "Vertical Drop",
                Value = _skiStatsRepository.GetVerticalDrop().Value,
                AthleteName = _skiStatsRepository.GetVerticalDrop().AthleteName
            }
        };

        var avg = _skiStatsRepository.GetTeamAverages();

        var vm = new TeamStatsViewModel
        {
            TeamRecords = records,
            TeamAverages = avg
        };

        return View(vm);
    }

    public IActionResult ViewAthleteStats(string id)
    {

        //Console.WriteLine($"Received Athlete ID: {id}");
        var athleteProfile = _skiStatsRepository.GetAthleteProfile(id);

        var athleteStats = new List<SkiStatsViewModel>
        {
            new SkiStatsViewModel
            {
                StatName = "Best Time",
                Value = _skiStatsRepository.GetAthleteBestTime(id).Value,
                AthleteName = _skiStatsRepository.GetAthleteBestTime(id).AthleteName,
                ProfilePictureBytes = athleteProfile.ProfilePictureBytes 
            },
            new SkiStatsViewModel
            {
                StatName = "Top Speed",
                Value = _skiStatsRepository.GetAthleteTopSpeed(id).Value,
                AthleteName = _skiStatsRepository.GetAthleteTopSpeed(id).AthleteName,
                ProfilePictureBytes = athleteProfile.ProfilePictureBytes 
            },
            new SkiStatsViewModel
            {
                StatName = "Best Distance",
                Value = _skiStatsRepository.GetAthleteBestDistance(id).Value,
                AthleteName = _skiStatsRepository.GetAthleteBestDistance(id).AthleteName,
                ProfilePictureBytes = athleteProfile.ProfilePictureBytes 
            },
            new SkiStatsViewModel
            {
                StatName = "Vertical Drop",
                Value = _skiStatsRepository.GetAthleteVerticalDrop(id).Value,
                AthleteName = _skiStatsRepository.GetAthleteVerticalDrop(id).AthleteName,
                ProfilePictureBytes = athleteProfile.ProfilePictureBytes 
            }
        };

        return View(athleteStats);
    }



}
