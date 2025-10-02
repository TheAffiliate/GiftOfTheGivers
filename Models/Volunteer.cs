using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers_ST10239864.Models
{   
    public class Volunteer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please provide your skills")]
        [StringLength(200)]
        public string Skills { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please indicate your availability")]
        [StringLength(100)]
        public string Availability { get; set; } = string.Empty;

        public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
        public string? AdminMessage { get; set; }
        public string? UserId { get; set; }
    }
}
