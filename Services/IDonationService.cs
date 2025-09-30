using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.Services
{
    public interface IDonationService
    {
        Task<IEnumerable<Donation>> GetAllAsync();
        Task<IEnumerable<Donation>> GetByUserIdAsync(string userId); 
        Task<Donation?> GetByIdAsync(int id);
        Task CreateAsync(Donation donation);
    }
}
