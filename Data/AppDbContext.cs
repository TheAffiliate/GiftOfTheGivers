using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Added 'virtual' keyword to all DbSet properties
        public virtual DbSet<IncidentReport> IncidentReports { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
    }
}