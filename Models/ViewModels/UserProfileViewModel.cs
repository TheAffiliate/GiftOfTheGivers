using System.Collections.Generic;
using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<Donation> Donations { get; set; } = new();
        public List<Volunteer> Volunteers { get; set; } = new();
        public List<IncidentReport> Incidents { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();
    }
}
