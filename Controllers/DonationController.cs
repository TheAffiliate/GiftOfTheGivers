using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    [Authorize] // ✅ Require login for all donation actions
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonationController(IDonationService donationService, UserManager<ApplicationUser> userManager)
        {
            _donationService = donationService;
            _userManager = userManager;
        }

        // -------------------------
        // List all donations (Admin sees all, user should ideally only see their own in Profile)
        // -------------------------
        public async Task<IActionResult> Index()
        {
            var donations = await _donationService.GetAllAsync();
            return View(donations);
        }

        // -------------------------
        // Create donation form
        // -------------------------
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (!ModelState.IsValid)
                return View(donation);

            // ✅ Link to logged-in user
            var user = await _userManager.GetUserAsync(User);
            donation.UserId = user?.Id;
            donation.Status = SubmissionStatus.Pending;

            await _donationService.CreateAsync(donation);
            return RedirectToAction(nameof(ThankYou));
        }

        // -------------------------
        // Thank you page after submit
        // -------------------------
        public IActionResult ThankYou()
        {
            return View();
        }

        // -------------------------
        // Donation details
        // -------------------------
        public async Task<IActionResult> Details(int id)
        {
            var donation = await _donationService.GetByIdAsync(id);
            if (donation == null)
                return NotFound();

            return View(donation);
        }
    }
}
