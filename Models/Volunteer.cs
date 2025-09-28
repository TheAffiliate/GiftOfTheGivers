using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }  // <-- Add this

        [Required]
        public string Skills { get; set; }

        public string Availability { get; set; }
    }
}
