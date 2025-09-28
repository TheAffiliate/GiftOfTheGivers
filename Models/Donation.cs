using System;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string DonorName { get; set; }
        public string ResourceType { get; set; }
        public int Quantity { get; set; }
        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;
    }
}