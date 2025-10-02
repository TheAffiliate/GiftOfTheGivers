using System;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }   
        public string Message { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}
