using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.Services
{
    public interface IDonationService
    {
        // Existing methods
        Task<IEnumerable<Donation>> GetAllAsync();
        Task<Donation?> GetByIdAsync(int id);
        Task CreateAsync(Donation donation);
        Task<IEnumerable<Donation>> GetByUserIdAsync(string userId);

        // ✅ REQUIRED BY UNIT TESTS: Fetch only approved donations
        Task<IEnumerable<Donation>> GetAllAcceptedAsync();

        // ✅ REQUIRED BY UNIT TESTS: Update the status of a donation
        Task UpdateStatusAsync(int id, SubmissionStatus status, string adminMessage);
    }
}
