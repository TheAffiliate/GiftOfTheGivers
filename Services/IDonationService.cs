using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.Services
{
    public interface IDonationService
    {
        Task<IEnumerable<Donation>> GetAllAsync();
        Task<Donation?> GetByIdAsync(int id);
        Task CreateAsync(Donation donation);
    }
}
