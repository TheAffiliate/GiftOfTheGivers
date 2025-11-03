using System.Collections.Generic;
using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<Donation> Donations { get; set; } = new();
        public List<Volunteer> Volunteers { get; set; } = new();
        public List<IncidentReport> Incidents { get; set; } = new();
    }
}
