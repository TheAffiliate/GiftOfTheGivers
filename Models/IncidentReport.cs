using System;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class IncidentReport
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
        public string? AdminMessage { get; set; }
        public string? UserId { get; set; }

    }
}
