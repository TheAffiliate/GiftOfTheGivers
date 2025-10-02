using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    [Authorize] // ✅ Require login for all volunteer actions
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService _volunteerService;
        private readonly UserManager<ApplicationUser> _userManager;

        public VolunteerController(IVolunteerService volunteerService, UserManager<ApplicationUser> userManager)
        {
            _volunteerService = volunteerService;
            _userManager = userManager;
        }

        // -------------------------
        // List all volunteers (admin-focused, but kept here for reference)
        // -------------------------
        public async Task<IActionResult> Index()
        {
            var volunteers = await _volunteerService.GetAllAsync();
            return View(volunteers);
        }

        // -------------------------
        // Create volunteer form (alias: Register)
        // -------------------------
        [HttpGet]
        public IActionResult Create()
        {
            return View("Register"); // use Register.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Volunteer volunteer)
        {
            if (!ModelState.IsValid)
                return View("Register", volunteer);

            // ✅ Link to logged-in user
            var user = await _userManager.GetUserAsync(User);
            volunteer.UserId = user?.Id;
            volunteer.Status = SubmissionStatus.Pending;

            await _volunteerService.CreateAsync(volunteer);
            TempData["Message"] = "Thank you for registering as a volunteer!";
            return RedirectToAction("Index", "Profile");
        }

        // -------------------------
        // Legacy Register route (maps to Create)
        // -------------------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Volunteer volunteer)
        {
            if (!ModelState.IsValid)
                return View(volunteer);

            // ✅ Link to logged-in user
            var user = await _userManager.GetUserAsync(User);
            volunteer.UserId = user?.Id;
            volunteer.Status = SubmissionStatus.Pending;

            await _volunteerService.CreateAsync(volunteer);
            TempData["Message"] = "Thank you for registering as a volunteer!";
            return RedirectToAction("Index", "Profile");
        }

        // -------------------------
        // Volunteer details
        // -------------------------
        public async Task<IActionResult> Details(int id)
        {
            var volunteer = await _volunteerService.GetByIdAsync(id);
            if (volunteer == null)
                return NotFound();

            return View(volunteer);
        }
    }
}
