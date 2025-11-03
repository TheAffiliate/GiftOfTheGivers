using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGivers_ST10239864.Data;
using GiftOfTheGivers_ST10239864.Models;
using GiftOfTheGivers_ST10239864.Services;
using System.Collections.Generic;

namespace GiftOfTheGivers_ST10239864.UnitTests
{
    public class VolunteerIntegrationTests
    {
        // Helper method to create a unique in-memory database context for each test
        private AppDbContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName + System.Guid.NewGuid().ToString())
                .Options;
            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task CreateAndRetrieveVolunteer_DataFlowVerification()
        {
            // Arrange
            using var context = GetInMemoryDbContext("VolunteerRegisterTest");

            // Assuming the concrete VolunteerService class exists and implements IVolunteerService
            var service = new VolunteerService(context);
            var volunteer = new Volunteer
            {
                FullName = "Alice Smith",
                Skills = "First Aid, Logistics",
                Availability = "Weekends",
                Email = "alice@example.com"
            };

            // Act 1: Create the Volunteer using the service's CreateAsync method
            await service.CreateAsync(volunteer);

            // Assert 1 (Data Layer Check): Verify the data was written to the database
            var savedVolunteer = await context.Volunteers.FirstOrDefaultAsync(v => v.Email == "alice@example.com");
            Assert.NotNull(savedVolunteer);
            Assert.Equal("First Aid, Logistics", savedVolunteer.Skills);

            // Act 2: Retrieve the saved data using a Service method (GetAllAsync)
            var retrievedVolunteers = await service.GetAllAsync();

            // Assert 2 (Service Layer Check): Verify the service can retrieve the data
            Assert.Single(retrievedVolunteers);
            Assert.Contains(retrievedVolunteers, v => v.FullName == "Alice Smith");
        }
    }
}