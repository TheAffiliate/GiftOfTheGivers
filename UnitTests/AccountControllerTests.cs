using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GiftOfTheGivers_ST10239864.Controllers;
using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Models.ViewModels;

namespace GiftOfTheGivers_ST10239864.UnitTests
{
    // NOTE: MockUserManager, MockSignInManager, and MockRoleManager definitions 
    // are placed in a separate shared file (e.g., SharedTestMocks.cs) 
    public class AccountControllerTests
    {
        private readonly Mock<MockUserManager> _mockUserManager;
        private readonly Mock<MockSignInManager> _mockSignInManager;
        private readonly Mock<MockRoleManager> _mockRoleManager;

        public AccountControllerTests()
        {
            // Initialization requires mock stores to satisfy the custom Mock constructors
            _mockUserManager = new Mock<MockUserManager>(new Mock<IUserStore<ApplicationUser>>().Object);
            _mockRoleManager = new Mock<MockRoleManager>(new Mock<IRoleStore<IdentityRole>>().Object);

            // FIX: The '!' operator suppresses the NRT warning as the Mock object will not be null
            _mockSignInManager = new Mock<MockSignInManager>(_mockUserManager.Object!);
        }

        [Fact]
        public async Task Login_ValidUserAdminRole_RedirectsToAdmin()
        {
            // Arrange
            _mockSignInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _mockUserManager.Setup(u => u.FindByEmailAsync("admin@test.com")).ReturnsAsync(new ApplicationUser());
            _mockUserManager.Setup(u => u.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Admin")).ReturnsAsync(true);

            var controller = new AccountController(_mockUserManager.Object, _mockSignInManager.Object, _mockRoleManager.Object);
            var model = new LoginViewModel { Email = "admin@test.com", Password = "Password123!" };

            // Act
            var result = await controller.Login(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Admin", result.ControllerName);
        }

        [Fact]
        public async Task Register_ValidModelAdminRole_AddsUserToAdminRole()
        {
            // Arrange
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Mock role existence check to ensure the logic runs
            _mockRoleManager.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Admin")).ReturnsAsync(IdentityResult.Success);

            var controller = new AccountController(_mockUserManager.Object, _mockSignInManager.Object, _mockRoleManager.Object);
            var model = new RegisterViewModel { Email = "admin@test.com", FullName = "Test Admin", Password = "Pass123!", ConfirmPassword = "Pass123!", RegisterAsAdmin = true };

            // Act
            var result = await controller.Register(model) as RedirectToActionResult;

            // Assert
            _mockUserManager.Verify(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Admin"), Times.Once());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Register_InvalidModel_ReturnsViewWithError()
        {
            // Arrange
            var controller = new AccountController(_mockUserManager.Object, _mockSignInManager.Object, _mockRoleManager.Object);
            var model = new RegisterViewModel { Email = "bad", Password = "1", ConfirmPassword = "2" };

            // Manually add model state error to simulate failed model validation
            controller.ModelState.AddModelError("Email", "Invalid format");

            // Act
            var result = await controller.Register(model) as ViewResult;

            // Assert
            _mockUserManager.Verify(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never());
            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
        }
    }
}