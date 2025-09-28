using GiftOfTheGivers_ST10239864.Models;
using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGivers_ST10239864.Services
{
    public interface IUserOperations
    {
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task UpdateProfileAsync(ApplicationUser user);

        // Authenticated Methods
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);
        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
    }
}
