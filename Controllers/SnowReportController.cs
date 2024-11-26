using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;

namespace CS330_Fall2024_FinalProject.Controllers;

public class SnowReportController : Controller
{
    private readonly SnowReportApplicationService _applicationService;

    public SnowReportController(SnowReportApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task<JsonResult> Report(string locationName)
    {
        if (string.IsNullOrEmpty(locationName))
        {
            return Json(new { error = "Location name not given." });
        }

        var report = await _applicationService.GetReportAsync(locationName);
        return Json(report);
    }
}
