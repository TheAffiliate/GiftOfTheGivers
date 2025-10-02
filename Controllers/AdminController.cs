using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _db;

    public AdminController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var model = new AdminDashboardViewModel
        {
            Donations = _db.Donations.ToList(),
            Volunteers = _db.Volunteers.ToList(),
            Incidents = _db.IncidentReports.ToList()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ReviewDonation(int id, bool approve, string? message)
    {
        var donation = await _db.Donations.FindAsync(id);
        if (donation == null) return NotFound();

        donation.Status = approve ? SubmissionStatus.Approved : SubmissionStatus.Rejected;
        donation.AdminMessage = message;
        await _db.SaveChangesAsync();

        // Add notification
        _db.Notifications.Add(new Notification
        {
            UserId = donation.UserId,
            Message = $"Your donation '{donation.Type}' was {(approve ? "approved ✅" : "rejected ❌")}. {message}",
            SentAt = DateTime.Now
        });
        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ReviewVolunteer(int id, bool approve, string? message)
    {
        var volunteer = await _db.Volunteers.FindAsync(id);
        if (volunteer == null) return NotFound();

        volunteer.Status = approve ? SubmissionStatus.Approved : SubmissionStatus.Rejected;
        volunteer.AdminMessage = message;
        await _db.SaveChangesAsync();

        // Add notification
        _db.Notifications.Add(new Notification
        {
            UserId = volunteer.UserId,
            Message = $"Your volunteer application was {(approve ? "approved ✅" : "rejected ❌")}. {message}",
            SentAt = DateTime.Now
        });
        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ReviewIncident(int id, bool approve, string? message)
    {
        var incident = await _db.IncidentReports.FindAsync(id);
        if (incident == null) return NotFound();

        incident.Status = approve ? SubmissionStatus.Approved : SubmissionStatus.Rejected;
        incident.AdminMessage = message;
        await _db.SaveChangesAsync();

        // Add notification
        _db.Notifications.Add(new Notification
        {
            UserId = incident.UserId,
            Message = $"Your incident report '{incident.Title}' was {(approve ? "approved ✅" : "rejected ❌")}. {message}",
            SentAt = DateTime.Now
        });
        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
