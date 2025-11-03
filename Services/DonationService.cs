using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GiftOfTheGivers_ST10239864.Services
{
    public class DonationService : IDonationService
    {
        private readonly AppDbContext _db;

        public DonationService(AppDbContext db)
        {
            _db = db;
        }

        // --- Existing User Methods ---

        public async Task<IEnumerable<Donation>> GetAllAsync() =>
            await _db.Donations.ToListAsync();

        public async Task<Donation?> GetByIdAsync(int id) =>
            await _db.Donations.FindAsync(id);

        public async Task CreateAsync(Donation donation)
        {
            _db.Donations.Add(donation);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Donation>> GetByUserIdAsync(string userId) =>
            await _db.Donations
                .Where(d => d.UserId == userId)
                .ToListAsync();

        // --- NEW Methods Required for Unit Tests ---

        // ✅ Implementation for GetAllAcceptedAsync (Fixes the compilation error)
        public async Task<IEnumerable<Donation>> GetAllAcceptedAsync() =>
            await _db.Donations
                .Where(d => d.Status == SubmissionStatus.Approved)
                .ToListAsync();

        // ✅ Implementation for UpdateStatusAsync (Required by AdminControllerTests)
        public async Task UpdateStatusAsync(int id, SubmissionStatus status, string adminMessage)
        {
            var donation = await _db.Donations.FindAsync(id);
            if (donation != null)
            {
                donation.Status = status;
                donation.AdminMessage = adminMessage;
                await _db.SaveChangesAsync();
            }
        }
    }
}