using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CS330_Fall2024_FinalProject.Controllers
{
    public class RoleController : Controller{
        
        //[Authorize(Policy = "AthleteOnly")] //Once it sees this tag, it will go to program.cs to check for the requirements of the athlete only policy
        [Authorize(Roles = $"{Constants.Roles.Athlete}")]
        public IActionResult Index(){
            return View();
        }

      //  [Authorize(Policy = "RequireManager")]
        //[Authorize(Roles = "Coach,Manager")] //Alternate way to do this
           [Authorize(Roles = $"{Constants.Roles.Manager}")]
        public IActionResult Manager()
        {
            return View();
        }

        // [Authorize(Policy = "RequireCoach")]
         [Authorize(Roles = $"{Constants.Roles.Coach}")]
         public IActionResult Coach()
        {
            return View();
        }
    }
}