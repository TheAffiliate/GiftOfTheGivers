using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        public string Item { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty; // e.g., Food, Clothing, Medical

        public DateTime DateDonated { get; set; } = DateTime.Now;

        // 🔑 Link to the user who donated
        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public string DonorName { get; set; } = string.Empty;
    }
}
