using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers_ST10239864.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly AppDbContext _db;

        public IncidentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<IncidentReport>> GetAllAsync() =>
            await _db.IncidentReports.ToListAsync();

        public async Task<IncidentReport?> GetByIdAsync(int id) =>
            await _db.IncidentReports.FindAsync(id);

        public async Task CreateAsync(IncidentReport incident)
        {
            _db.IncidentReports.Add(incident);
            await _db.SaveChangesAsync();
        }
    }
}
