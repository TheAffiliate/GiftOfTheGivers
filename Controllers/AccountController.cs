using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Models.ViewModels;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserOperations _userOperations;

        public AccountController(IUserOperations userOperations)
        {
            _userOperations = userOperations;
        }

        // -------------------------
        // Register
        // -------------------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Location = model.Location
            };

            var result = await _userOperations.RegisterAsync(user, model.Password); // <-- pass password

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // -------------------------
        // Login
        // -------------------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userOperations.LoginAsync(model.Email, model.Password, model.RememberMe); // <-- pass password

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // -------------------------
        // Logout
        // -------------------------
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userOperations.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
