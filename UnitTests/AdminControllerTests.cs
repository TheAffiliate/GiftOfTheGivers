using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using GiftOfTheGivers_ST10239864.Controllers;
using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GiftOfTheGivers_ST10239864.UnitTests
{
    public class AdminControllerTests
    {
        private readonly Mock<AppDbContext> _mockDbContext;

        public AdminControllerTests()
        {
            // Initialize the DbContext mock, passing dummy options to the base constructor
            _mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        }

        // --- EF Core Mocking Helpers ---

        // Helper to mock DbSet properties for IQueryable operations (e.g., .ToList())
        private Mock<DbSet<T>> MockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        // FIX: Updated to return ValueTask<T?> to match the DbSet.FindAsync signature and resolve the nullability warning.
        private void SetupFindAsync<T>(Mock<DbSet<T>> mockSet, T entity) where T : class
        {
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>()))
                   .Returns<object[]>(keyValues =>
                       // Cast entity to the nullable reference type T?
                       new ValueTask<T?>(Task.FromResult<T?>(entity)));
        }

        // --- Test Methods ---

        [Fact]
        public void Index_ReturnsViewWithAdminDashboardViewModel()
        {
            // Arrange
            var donations = new List<Donation> { new Donation { Id = 1, Quantity = 10 } }.AsQueryable();
            var volunteers = new List<Volunteer> { new Volunteer { Id = 1, FullName = "John" } }.AsQueryable();
            var incidents = new List<IncidentReport>().AsQueryable();

            // Note: Using the ! operator to suppress NRT warnings on DbSets
            _mockDbContext.Setup(c => c.Donations!).Returns(MockDbSet(donations).Object);
            _mockDbContext.Setup(c => c.Volunteers!).Returns(MockDbSet(volunteers).Object);
            _mockDbContext.Setup(c => c.IncidentReports!).Returns(MockDbSet(incidents).Object);

            var controller = new AdminController(_mockDbContext.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<AdminDashboardViewModel>(result.Model);

            // Using recommended Assert.Single() for size 1 collections
            Assert.Single(model.Donations);
            Assert.Single(model.Volunteers);
        }

        [Fact]
        public async Task ReviewDonation_Approve_UpdatesStatusAndAddsNotification()
        {
            // Arrange
            const int donationId = 5;
            var donationToReview = new Donation { Id = donationId, UserId = "user1", Type = "Books", Status = SubmissionStatus.Pending };
            var donations = new List<Donation> { donationToReview }.AsQueryable();

            var mockDonationSet = MockDbSet(donations);
            SetupFindAsync(mockDonationSet, donationToReview);
            _mockDbContext.Setup(c => c.Donations!).Returns(mockDonationSet.Object);

            var mockNotificationSet = new Mock<DbSet<Notification>>();
            _mockDbContext.Setup(c => c.Notifications!).Returns(mockNotificationSet.Object);
            _mockDbContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var controller = new AdminController(_mockDbContext.Object);

            // Act
            var result = await controller.ReviewDonation(id: donationId, approve: true, message: "Approved by Admin");

            // Assert
            Assert.Equal(SubmissionStatus.Approved, donationToReview.Status);

            _mockDbContext.Verify(c => c.SaveChangesAsync(default), Times.Exactly(2));

            mockNotificationSet.Verify(m => m.Add(It.Is<Notification>(n =>
                n.UserId == "user1" &&
                n.Message.Contains("approved ✅")
            )), Times.Once());

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task ReviewVolunteer_Reject_UpdatesStatusAndAddsNotification()
        {
            // Arrange
            const int volunteerId = 10;
            var volunteerToReview = new Volunteer { Id = volunteerId, UserId = "user2", FullName = "Bob", Status = SubmissionStatus.Pending };
            var volunteers = new List<Volunteer> { volunteerToReview }.AsQueryable();

            var mockVolunteerSet = MockDbSet(volunteers);
            SetupFindAsync(mockVolunteerSet, volunteerToReview);
            _mockDbContext.Setup(c => c.Volunteers!).Returns(mockVolunteerSet.Object);

            var mockNotificationSet = new Mock<DbSet<Notification>>();
            _mockDbContext.Setup(c => c.Notifications!).Returns(mockNotificationSet.Object);
            _mockDbContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var controller = new AdminController(_mockDbContext.Object);

            // Act
            var result = await controller.ReviewVolunteer(id: volunteerId, approve: false, message: "Needs more info");

            // Assert
            Assert.Equal(SubmissionStatus.Rejected, volunteerToReview.Status);

            _mockDbContext.Verify(c => c.SaveChangesAsync(default), Times.Exactly(2));

            mockNotificationSet.Verify(m => m.Add(It.Is<Notification>(n =>
                n.UserId == "user2" &&
                n.Message.Contains("rejected ❌")
            )), Times.Once());

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}