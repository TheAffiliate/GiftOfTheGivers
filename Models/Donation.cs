using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DonorName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // e.g., Food, Clothes, Medicine

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public DateTime DateDonated { get; set; } = DateTime.Now;

        // Optional: link donations to a logged-in user
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
        public string? AdminMessage { get; set; }
    }
}
