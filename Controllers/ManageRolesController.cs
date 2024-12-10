using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Bcpg;

namespace CS330_Fall2024_FinalProject.Controllers
{
    [Authorize(Roles = "Coach")]
    public class ManageRolesController : Controller
    {
        private readonly UserManager<Athlete> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRolesController(UserManager<Athlete> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new UserRoleViewModel
                {
                    UserId = await _userManager.GetUserIdAsync(user),
                    Email = user.Email?.ToString() ?? "No Email Provided",
                    // Email = user.Email,
                    Roles = roles.ToList()
                });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
        
            if(await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return RedirectToAction("Index");
        
        }
    }
}
