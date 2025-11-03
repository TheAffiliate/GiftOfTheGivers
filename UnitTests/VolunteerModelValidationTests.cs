using Xunit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.UnitTests
{
    public class VolunteerModelValidationTests
    {
        // Helper method to perform validation using DataAnnotations
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void VolunteerModel_ValidData_PassesValidation()
        {
            // Arrange
            var model = new Volunteer
            {
                FullName = "Jane Doe",
                Skills = "Driving, First Aid",
                Availability = "Mornings",
                Email = "jane.doe@example.com"
            };

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void VolunteerModel_RequiredFieldsMissing_FailsValidation()
        {
            // Arrange
            // Note: This relies on [Required] attributes being present in your Volunteer model
            var model = new Volunteer
            {
                FullName = "", // FIX: Changed null to "" to satisfy NRT checking
                Skills = "None",
                Availability = "N/A",
                Email = "" // Likely caught by [Required]
            };

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.True(results.Count >= 1);
            Assert.Contains(results, r => r.MemberNames.Contains("FullName") || r.MemberNames.Contains("Email"));
        }
    }
}