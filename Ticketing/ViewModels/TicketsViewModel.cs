using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using Ticketing.Models;

namespace Ticketing.ViewModels
{
    public class TicketsViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Severity { get; set; }
        public DateTime IssueStartDate { get; set; } = DateTime.Now;
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int SLInHours { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? CreatorId { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public string? LastUpdatedById { get; set; }
        public DateTime LastActionAt { get; set; }
        public DateTime SLEndDateTime => IssueStartDate.AddHours(SLInHours);
        public string? TechnicianId { get; set; }
        public string? Comment { get; set; }
    }
}
