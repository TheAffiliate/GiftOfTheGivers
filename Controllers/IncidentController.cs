using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    public class IncidentController : Controller
    {
        private readonly IIncidentService _incidentService;
        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        public async Task<IActionResult> Index()
        {
            var incidents = await _incidentService.GetAllAsync();
            return View(incidents);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentReport report)
        {
            if (!ModelState.IsValid) return View(report);
            await _incidentService.CreateAsync(report);
            TempData["Message"] = "Thank you — incident reported.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var incident = await _incidentService.GetByIdAsync(id);
            if (incident == null) return NotFound();
            return View(incident);
        }
    }
}
