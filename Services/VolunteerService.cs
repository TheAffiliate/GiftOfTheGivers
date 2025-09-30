using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers_ST10239864.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly AppDbContext _db;

        public VolunteerService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Volunteer>> GetAllAsync() =>
            await _db.Volunteers.ToListAsync();

        public async Task<Volunteer?> GetByIdAsync(int id) =>
            await _db.Volunteers.FindAsync(id);

        public async Task CreateAsync(Volunteer volunteer)
        {
            _db.Volunteers.Add(volunteer);
            await _db.SaveChangesAsync();
        }
    }
}
