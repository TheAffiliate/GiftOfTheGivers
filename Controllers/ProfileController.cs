using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;

        public ProfileController(UserManager<ApplicationUser> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // Default profile page: /Profile/Index
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var donations = await _db.Donations
                .Where(d => d.UserId == user.Id)
                .ToListAsync();

            var volunteers = await _db.Volunteers
                .Where(v => v.UserId == user.Id)
                .ToListAsync();

            var incidents = await _db.IncidentReports
                .Where(i => i.UserId == user.Id)
                .ToListAsync();

            var notifications = await _db.Notifications
                .Where(n => n.UserId == user.Id)
                .OrderByDescending(n => n.SentAt)
                .ToListAsync();

            var model = new UserProfileViewModel
            {
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Donations = donations,
                Volunteers = volunteers,
                Incidents = incidents,
                Notifications = notifications
            };

            return View(model);
        }

        // Alias: /Account/ManageProfile
        [HttpGet("/Account/ManageProfile")]
        public IActionResult ManageProfile()
        {
            // Reuse the Index action
            return RedirectToAction("Index");
        }

        // Inbox view: /Profile/Inbox
        public async Task<IActionResult> Inbox()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var notifications = await _db.Notifications
                .Where(n => n.UserId == user.Id)
                .OrderByDescending(n => n.SentAt)
                .ToListAsync();

            return View(notifications);
        }
    }
}
