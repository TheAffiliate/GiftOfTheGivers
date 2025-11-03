using System;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class IncidentReport
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
        public string? AdminMessage { get; set; }
        public string? UserId { get; set; }

    }
}
