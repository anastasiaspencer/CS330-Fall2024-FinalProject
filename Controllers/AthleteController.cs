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

        [Authorize(Roles = "Coach")]
        public IActionResult Index()
        {
            var athletes = _unitOfWork.Athlete.GetUser();
            return View(athletes);
        }
 
        [HttpPost]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> Delete(string id)
        {
            Console.WriteLine($"Delete Method Received ID: {id}");

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty.");
            }

            var athlete = _unitOfWork.Athlete.GetUser(id);
            if (athlete == null)
            {
                return NotFound($"Athlete with ID {id} not found.");
            }

            await _unitOfWork.Athlete.Delete(athlete);

            // Redirect to the Index action (or another view)
            return RedirectToAction("Index");
        }


        public IActionResult DeleteUser(string id)
        {
            try
            {
                var athlete = _unitOfWork.Athlete.GetUser(id);

                _unitOfWork.Athlete.Delete(athlete);

            }
            catch
            {
                return BadRequest("Athlete could not be deleted.");
            }

            return View("Index");

        }

        [Authorize(Roles = "Coach")]
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

        [Authorize(Roles = "Coach")]
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

            athlete.Birthday = data.Athlete.Birthday;
            //athlete.ProfilePicture = data.Athlete.ProfilePicture;
            athlete.Name = data.Athlete.Name;
            athlete.Number = data.Athlete.Number;
            athlete.SkiLevel = data.Athlete.SkiLevel;
            //athlete.Email = data.Athlete.Email;

            _unitOfWork.Athlete.UpdateAthlete(athlete);

            return RedirectToAction("Index", new { id = athlete.Id });
        }


    }
}