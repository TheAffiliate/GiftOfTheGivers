using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("FullName")] // Maps to the existing column in DB
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Column("Email")] // Maps to the existing column in DB
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Skills { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("Availability")] // Maps to the existing column in DB
        public string? Availability { get; set; }
    }
}
