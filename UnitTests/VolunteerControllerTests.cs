using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using GiftOfTheGivers_ST10239864.Controllers;
using GiftOfTheGivers_ST10239864.Services;
using GiftOfTheGivers_ST10239864.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewFeatures; // ⬅️ NEW IMPORT for TempData

namespace GiftOfTheGivers_ST10239864.UnitTests
{
    public class VolunteerControllerTests
    {
        private readonly Mock<IVolunteerService> _mockVolunteerService = new Mock<IVolunteerService>();
        private readonly Mock<MockUserManager> _mockUserManager;

        // Define a constant for the test user ID
        private const string TestUserId = "test-user-id-123";

        public VolunteerControllerTests()
        {
            // Initialize the mock UserManager
            _mockUserManager = new Mock<MockUserManager>(new Mock<IUserStore<ApplicationUser>>().Object);
        }

        [Fact]
        public async Task Create_ValidModel_LinksToUserAndRedirectsToProfile()
        {
            // Arrange
            var mockUser = new ApplicationUser { Id = TestUserId };

            // 1. Setup the mock UserManager to return the mockUser when called with any ClaimsPrincipal
            _mockUserManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(mockUser);

            var controller = new VolunteerController(_mockVolunteerService.Object, _mockUserManager.Object);

            // 2. Setup ClaimsPrincipal with NameIdentifier
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, TestUserId),
            }, "mock"));

            // 3. Attach the ClaimsPrincipal to the ControllerContext
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // 4.Initialize TempData to prevent NullReferenceException when accessing TempData["Message"]
            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
            );

            var validVolunteer = new Volunteer
            {
                FullName = "New Volunteer",
                Skills = "Coding",
                Availability = "Anytime",
                Email = "volunteer@test.com"
            };

            // Act
            var result = await controller.Create(validVolunteer) as RedirectToActionResult;

            // Assert
            // Verify that the service was called with a Volunteer linked to the correct user ID
            _mockVolunteerService.Verify(s => s.CreateAsync(
                It.Is<Volunteer>(v => v.UserId == TestUserId && v.Status == SubmissionStatus.Pending)
            ), Times.Once());

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Profile", result.ControllerName);
        }

        [Fact]
        public async Task Register_InvalidModel_ReturnsViewWithError()
        {
            // Arrange
            var controller = new VolunteerController(_mockVolunteerService.Object, _mockUserManager.Object);

            // FIX: Ensure non-nullable properties are initialized to pass C# compiler checks.
            var invalidVolunteer = new Volunteer
            {
                FullName = string.Empty,
                Skills = "Too Long",
                Availability = string.Empty,
                Email = "invalid-email"
            };

            controller.ModelState.AddModelError("FullName", "Required");
            controller.ModelState.AddModelError("Email", "Invalid Email Format");

            // Act
            var result = await controller.Register(invalidVolunteer) as ViewResult;

            // Assert
            _mockVolunteerService.Verify(s => s.CreateAsync(It.IsAny<Volunteer>()), Times.Never());

            Assert.NotNull(result);
            // Asserts that the view returned the same invalid model object
            Assert.Equal(invalidVolunteer, result.Model);
        }
    }
}