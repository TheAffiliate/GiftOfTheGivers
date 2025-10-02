using System.Collections.Generic;
using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<Donation> Donations { get; set; }
        public List<Volunteer> Volunteers { get; set; }
        public List<IncidentReport> Incidents { get; set; }
    }
}
