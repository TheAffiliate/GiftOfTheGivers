using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService _volunteerService;

        public VolunteerController(IVolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
        }

        // -------------------------
        // List all volunteers
        // -------------------------
        public async Task<IActionResult> Index()
        {
            var volunteers = await _volunteerService.GetAllAsync();
            return View(volunteers);
        }

        // -------------------------
        // Volunteer registration form (GET)
        // -------------------------
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Uses Register.cshtml
        }

        // -------------------------
        // Volunteer registration submission (POST)
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Volunteer volunteer)
        {
            if (!ModelState.IsValid)
                return View(volunteer); // Returns to Register.cshtml with validation messages

            await _volunteerService.CreateAsync(volunteer);
            TempData["Message"] = "Thank you for registering as a volunteer!";
            return RedirectToAction(nameof(Index));
        }

        // -------------------------
        // Volunteer details
        // -------------------------
        public async Task<IActionResult> Details(int id)
        {
            var volunteer = await _volunteerService.GetByIdAsync(id);
            if (volunteer == null)
                return NotFound();

            return View(volunteer); // Requires Details.cshtml
        }
    }
}
