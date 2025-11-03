using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;

namespace GiftOfTheGivers_ST10239864.UnitTests
{
    public class DonationServiceTests
    {
        private AppDbContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName + System.Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAcceptedAsync_FiltersByApprovedStatus()
        {
            // Arrange
            using var context = GetInMemoryDbContext("TestDB_FilterAccepted");

            // Uses Quantity and Status, matching your Donation.cs
            context.Donations.Add(new Donation { Id = 1, Quantity = 10, Status = SubmissionStatus.Approved, DonorName = "A" });
            context.Donations.Add(new Donation { Id = 2, Quantity = 5, Status = SubmissionStatus.Pending, DonorName = "B" });
            context.Donations.Add(new Donation { Id = 3, Quantity = 20, Status = SubmissionStatus.Approved, DonorName = "C" });
            await context.SaveChangesAsync();

            var service = new DonationService(context);

            // Act
            var acceptedDonations = await service.GetAllAcceptedAsync(); // Now compiles

            // Assert
            Assert.Equal(2, acceptedDonations.Count());
            Assert.DoesNotContain(acceptedDonations, d => d.Status == SubmissionStatus.Pending);
        }

        [Fact]
        public async Task CreateAsync_SetsDateAndPendingStatusByDefault()
        {
            // Arrange
            using var context = GetInMemoryDbContext("TestDB_AddDonation");
            var service = new DonationService(context);

            var newDonation = new Donation { Type = "Medicine", Quantity = 10, DonorName = "Test" };

            // Act
            await service.CreateAsync(newDonation); // Compiles

            // Assert
            var savedDonation = await context.Donations.FirstAsync();
            Assert.Equal(SubmissionStatus.Pending, savedDonation.Status);
            Assert.True(savedDonation.DateDonated.Date == System.DateTime.Now.Date);
        }
    }
}