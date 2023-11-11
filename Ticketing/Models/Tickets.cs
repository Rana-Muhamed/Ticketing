using Microsoft.Build.Framework;

namespace Ticketing.Models
{
    public class Tickets
    {
        [Required]
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Severity { get; set; }
        public DateTime IssueStartDate { get; set; }= DateTime.Now;
        public int SLInHours { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } = "New";
        public string? CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public string? LastUpdatedById { get; set; }
        public ApplicationUser? LastUpdatedBy { get; set; }
        public DateTime LastActionAt { get; set; } = DateTime.Now;
        public DateTime SLEndDateTime => IssueStartDate.AddHours(SLInHours);
        public string? TechnicianId { get; set; }
        public ApplicationUser? Technician { get; set; }
        public string? Comment { get; set; }

    }
}
