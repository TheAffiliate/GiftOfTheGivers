using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers_ST10239864.Services
{
    public class DonationService : IDonationService
    {
        private readonly AppDbContext _db;

        public DonationService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Donation>> GetAllAsync() =>
            await _db.Donations
                     .Include(d => d.User) // include user info for admin view
                     .ToListAsync();

        public async Task<IEnumerable<Donation>> GetByUserIdAsync(string userId) =>
            await _db.Donations
                     .Where(d => d.UserId == userId)
                     .Include(d => d.User)
                     .ToListAsync();

        public async Task<Donation?> GetByIdAsync(int id) =>
            await _db.Donations
                     .Include(d => d.User)
                     .FirstOrDefaultAsync(d => d.Id == id);

        public async Task CreateAsync(Donation donation)
        {
            _db.Donations.Add(donation);
            await _db.SaveChangesAsync();
        }
    }
}
