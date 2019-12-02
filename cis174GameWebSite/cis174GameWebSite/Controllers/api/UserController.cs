using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cis174GameWebSite.Services;
using cis174GameWebSite.Models;
using cis174GameWebSite.Models.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using cis174GameWebSite.Data;

namespace cis174GameWebSite.Controllers.api
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public UserController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpPut]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Ok(
                            _context.ApplicationUsers
                                .Where(a => a.UserName == model.Email)
                                .Select(a => new ApplicationUser
                                {
                                    Id = a.Id
                                })
                            );
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return BadRequest("Account locked out");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    _logger.LogError("Invalid login attempt");
                    return BadRequest("Invalid attempt");
                }
            }

            

            // If we got this far, something failed, redisplay form
            _logger.LogWarning("Login failed, invalid");
            return BadRequest("Invalid attempt");
        }

        // POST: api/Register
        [HttpPut]
        [Route("reg")]
        public async Task<IActionResult> PostAsync([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return Ok(
                    _context.ApplicationUsers
                        .Where(a => a.UserName == model.Email)
                        .Select(a => new ApplicationUser
                        {
                            Id = a.Id
                        })
                    );
                }
                _logger.LogError("Could not register");
                return BadRequest();
            }

            _logger.LogError("Invalid model issue");
            // If we got this far, something failed, redisplay form
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("lockout")]
        public IActionResult Lockout()
        {
            _logger.LogError("Locked out");
            return Ok("Locked out");
        }

        [HttpPut]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Ok(new string("Logout Successful"));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("extLoginCallB")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                _logger.LogError($"Error from external provider: {remoteError}");
                return BadRequest("Error from external provider");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                _logger.LogWarning("Info is empty");
                return BadRequest("Info is empty");
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return Ok(new string("Login Successful"));
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User locked out");
                return BadRequest("User locked out");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                _logger.LogWarning("No account found, please create an account");
                return BadRequest("No account found, please create an account");
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("extLoginConf")]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    _logger.LogError("Error loading external login information during confirmation.");
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return Ok(
                        _context.ApplicationUsers
                            .Where(a => a.UserName == model.Email)
                            .Select(a => new ApplicationUser
                            {
                                Id = a.Id
                            })
                        );
                    }
                }
                return BadRequest("Login failed");
            }
            _logger.LogWarning("Login failed, invalid");
            return BadRequest("Login failed, invalid");
        }
    }
}
