using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.Services
{
    public interface IVolunteerService
    {
        Task<IEnumerable<Volunteer>> GetAllAsync();
        Task<Volunteer?> GetByIdAsync(int id);
        Task CreateAsync(Volunteer volunteer);
    }
}
