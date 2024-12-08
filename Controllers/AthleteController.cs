using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CS330_Fall2024_FinalProject.Core.Repositories;
using CS330_Fall2024_FinalProject.Core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using CS330_Fall2024_FinalProject.Models;


namespace CS330_Fall2024_FinalProject.Controllers
{
    public class AthleteController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<Athlete> _signInManager;
        public AthleteController(IUnitOfWork unitOfWork, SignInManager<Athlete> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var athletes = _unitOfWork.Athlete.GetUser();
            return View(athletes);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var athlete = _unitOfWork.Athlete.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();
            var userRoles = await _signInManager.UserManager.GetRolesAsync(athlete);

            // var roleItems = roles.Select(role =>
            // new SelectListItem(role.Name, role.Id, userRoles.Any(ur => ur.Contains(role.Name)))).ToList();
            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur != null && role.Name != null && ur.Contains(role.Name))
                )
            ).ToList();

            var vm = new EditAthleteViewModel
            {
                Athlete = athlete,
                Roles = roleItems
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditAthleteViewModel data)
        {

            if (data.Athlete == null || string.IsNullOrEmpty(data.Athlete.Id))
            {
                return BadRequest("Invalid athlete data.");
            }

            var athlete = _unitOfWork.Athlete.GetUser(data.Athlete.Id);
            if (athlete == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(athlete);

            //Loop through the roles in ViewModel
            //Check if the Role is Assinged in DB
            //If assigned, do nothing
            // If not assigned add role

            if (data.Roles == null || !data.Roles.Any())
            {
                return BadRequest("Roles cannot be null or empty.");
            }

            foreach (var role in data.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    //add role
                    if (assignedInDb == null)
                    {
                        await _signInManager.UserManager.AddToRoleAsync(athlete, role.Text);
                    }
                }
                else
                {
                    //remove role
                    if (assignedInDb != null)
                    {
                        await _signInManager.UserManager.RemoveFromRoleAsync(athlete, role.Text);
                    }
                }
            }

            athlete.ProfilePicture = data.Athlete.ProfilePicture;
            athlete.Birthday = data.Athlete.Birthday;
            athlete.Name = data.Athlete.Name;
            athlete.Number = data.Athlete.Number;
            athlete.Email = data.Athlete.Email;

            _unitOfWork.Athlete.UpdateAthlete(athlete);

            return RedirectToAction("Edit", new {id = athlete.Id});
        }


    }
}