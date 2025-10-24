using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NSCC_WebAppProg_SeatYourself.Controllers
{
    // Move the field and constructor inside the AccountController class
    public class AccountController : Controller
    {
        private IConfiguration _configuration;

        // Constructor to inject IConfiguration
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        //Post: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            // For demonstration purposes, we use hardcoded credentials.
            // In a real application, you would validate against a database.
            if (username == _configuration["occasions_username"] && password == _configuration["occasions_password"])
            {
                // Create a list of claims identifying the user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Name, "Administrator"), //Username
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                string? returnUrl = Request.Query["ReturnUrl"]; //Get the return URL if any from the query string

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirmed()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}

