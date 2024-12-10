// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace CS330_Fall2024_FinalProject.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Athlete> _signInManager;
        private readonly UserManager<Athlete> _userManager;
        private readonly IUserStore<Athlete> _userStore;
        private readonly IUserEmailStore<Athlete> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<Athlete> userManager,
            IUserStore<Athlete> userStore,
            SignInManager<Athlete> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            [CustomValidation(typeof(InputModel), nameof(ValidateEmail))]
            [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)?ua\.edu$", ErrorMessage = "Only University of Alabama email addresses are allowed.")]

            // the below regular expression is checking pattern=“.+@+(.+\.)?ua/.edu” to ensure that only UA emails are being registered
            // cybersecurity check to ensure the site will not get filled with spam emails. Additionally, this flow sends a confirmation email to the registered email to confirm account, else they cannot login
            // therefore, fake UA emails (fake@crimson.ua.edu for example) can never login to the website as they cannot click on the link sent to fake@crimson.ua.edu to verify their account

            // this is a double check, this one lets the user know while they type that this is not allowed
            // the second check is below, when they press to register.

            // brian's personal note while coding since navbar currently doesn't have login partial (_Layout.cshtml)
            //http://localhost:5121/Identity/Account/Register is how i access registration page
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Number")]
            public int Number { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Birthday")]
            public DateTime Birthday { get; set; }

            [Required]
            [Display(Name = "Ski Level")]

            public string SkiLevel { get; set; }

            [Required]
            [Display(Name = "Best Time")]
            public double BestTime { get; set; }

            [Required]
            [Display(Name = "Top Speed")]
            public double TopSpeed { get; set; }

            [Required]
            [Display(Name = "Best Distance")]
            public double BestDistance { get; set; }

            [Required]
            [Display(Name = "Vertical Drop")]
            public double VerticalDrop { get; set; }

            [Required]
            [Display(Name = "Ski Ranking")]
            public double Ranking { get; set; }

            [Display(Name = "Profile Picture")]
            public IFormFile ProfilePicture { get; set; }

            public byte[]ProfilePictureBytes { get; set; } // For display

            public static ValidationResult ValidateEmail(string email, ValidationContext context)
            {
                if (string.IsNullOrEmpty(email))
                {
                    return new ValidationResult("Email is required.");
                }

                var uaEduRegex = new System.Text.RegularExpressions.Regex(@"^[^+]+@([a-zA-Z0-9-]+\.)?ua\.edu$");
                if (!uaEduRegex.IsMatch(email))
                {
                    return new ValidationResult("Email must be a valid .ua.edu address and cannot contain +<number> before @.");
                }

                return ValidationResult.Success;
            }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                user.Name = $"{Input.FirstName} {Input.LastName}";
                user.Number = Input.Number;
                user.Birthday = Input.Birthday;
                user.SkiLevel = Input.SkiLevel;
               

                user.Stats = new SkiStats
                {
                    BestTime = Input.BestTime,
                    TopSpeed = Input.TopSpeed,
                    BestDistance = Input.BestDistance,
                    VerticalDrop = Input.VerticalDrop,
                    Ranking = Input.Ranking
                };

                if (Input.ProfilePicture != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Input.ProfilePicture.CopyToAsync(memoryStream);
                        user.ProfilePicture = memoryStream.ToArray(); // Save the picture as a byte array
                         Input.ProfilePictureBytes = user.ProfilePicture;
                    }
                }

                


                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "Athlete");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "Confirm your email",
                        $@"
                            <div style='font-family: Arial, sans-serif; color: #333;'>
                                <h3 style='color: #49A097; margin-bottom: 20px;'>Hello,</h3>
                                <p style='font-size: 16px; line-height: 1.5;'>
                                    Thank you for registering with us! Please confirm your email address by clicking the link below:
                                </p>
                                <div style='background-color: #f4f4f4; padding: 15px; border-radius: 8px; margin: 20px 0; text-align: center;'>
                                    <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='display: inline-block; padding: 10px 20px; font-size: 16px; color: #fff; background-color: #49A097; text-decoration: none; border-radius: 5px;'>
                                        Confirm Email Address
                                    </a>
                                </div>
                                <p style='font-size: 16px; line-height: 1.5;'>
                                    If you did not register for this account, you can ignore this email.
                                </p>
                                <p style='font-size: 16px; line-height: 1.5; margin-top: 20px;'>
                                    Best regards,<br>
                                    <span style='font-weight: bold; color: #49A097;'>Snow Ski</span>
                                </p>
                                <hr style='border: none; border-top: 1px solid #49A097; margin-top: 30px;'/>
                                
                            </div>"
                    );
                    // ommited paragraph, goes at bottom of email to show link. but need deployed link later
                    // <p style='font-size: 12px; color: #777;'>
                    //     This email was sent from <strong>Snow Ski</strong>. To learn more, visit
                    //     <a href='https://jeongbinson.com/' style='color: #49A097; text-decoration: none;'>https://jeongbinson.com/</a>.
                    // </p>


                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }


        private Athlete CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Athlete>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Athlete)}'. " +
                    $"Ensure that '{nameof(Athlete)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Athlete> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Athlete>)_userStore;
        }
    }
}
