using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService _volService;
        public VolunteerController(IVolunteerService volService) { _volService = volService; }

        public async Task<IActionResult> Index()
        {
            var volunteers = await _volService.GetAllAsync();
            return View(volunteers);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Volunteer v)
        {
            if (!ModelState.IsValid) return View(v);
            await _volService.RegisterAsync(v);
            TempData["Message"] = "Thank you for registering as a volunteer!";
            return RedirectToAction("Index");
        }
    }
}
