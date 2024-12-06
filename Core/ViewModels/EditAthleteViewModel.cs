using CS330_Fall2024_FinalProject.Controllers;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CS330_Fall2024_FinalProject.Core.ViewModels
{
    public class EditAthleteViewModel
    {
        // public Athlete Athlete { get; set; }

        public Athlete? Athlete { get; set; }

        // public IList<SelectListItem> Roles { get; set; }

        public IList<SelectListItem>? Roles { get; set; }

    }
}