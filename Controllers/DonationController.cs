using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;
        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        public async Task<IActionResult> Index()
        {
            var donations = await _donationService.GetAllAsync();
            return View(donations);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (!ModelState.IsValid) return View(donation);
            await _donationService.CreateAsync(donation);
            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var donation = await _donationService.GetByIdAsync(id);
            if (donation == null) return NotFound();
            return View(donation);
        }
    }
}
