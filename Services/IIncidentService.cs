using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.Services
{
    public interface IIncidentService
    {
        Task<IEnumerable<IncidentReport>> GetAllAsync();
        Task<IncidentReport?> GetByIdAsync(int id);
        Task CreateAsync(IncidentReport incident);
    }
}
