using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers_ST10239864.Services
{
    public class TUserOperations : IUserOperations
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _db;

        public TUserOperations(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id) =>
            await _userManager.FindByIdAsync(id);

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync() =>
            await _db.Users.ToListAsync();

        public async Task UpdateProfileAsync(ApplicationUser user)
        {
            var existing = await _userManager.FindByIdAsync(user.Id);
            if (existing == null) throw new Exception("User not found");
            existing.FullName = user.FullName;
            existing.Location = user.Location;
            await _userManager.UpdateAsync(existing);
        }

        // New methods
        public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
