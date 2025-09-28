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
            await _db.Donations.ToListAsync();

        public async Task<Donation?> GetByIdAsync(int id) =>
            await _db.Donations.FindAsync(id);

        public async Task CreateAsync(Donation donation)
        {
            _db.Donations.Add(donation);
            await _db.SaveChangesAsync();
        }
    }
}
