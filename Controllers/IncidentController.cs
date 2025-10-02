using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    [Authorize] // ✅ Require login for all incident actions
    public class IncidentController : Controller
    {
        private readonly IIncidentService _incidentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncidentController(IIncidentService incidentService, UserManager<ApplicationUser> userManager)
        {
            _incidentService = incidentService;
            _userManager = userManager;
        }

        // -------------------------
        // List all incident reports (admin-only context, but safe for now)
        // -------------------------
        public async Task<IActionResult> Index()
        {
            var incidents = await _incidentService.GetAllAsync();
            return View(incidents);
        }

        // -------------------------
        // Report an incident
        // -------------------------
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentReport report)
        {
            if (!ModelState.IsValid) return View(report);

            // ✅ Link report to logged-in user
            var user = await _userManager.GetUserAsync(User);
            report.UserId = user?.Id;
            report.Status = SubmissionStatus.Pending;

            await _incidentService.CreateAsync(report);
            TempData["Message"] = "Thank you — incident reported.";
            return RedirectToAction("Index", "Profile"); // ✅ redirect to profile so user sees their submission
        }

        // -------------------------
        // Incident details
        // -------------------------
        public async Task<IActionResult> Details(int id)
        {
            var incident = await _incidentService.GetByIdAsync(id);
            if (incident == null) return NotFound();
            return View(incident);
        }
    }
}
