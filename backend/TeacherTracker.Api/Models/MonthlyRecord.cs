using System.ComponentModel.DataAnnotations;
using TeacherTracker.Api.Enums;

namespace TeacherTracker.Api.Models
{
    public class MonthlyRecord
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int SchoolId { get; set; }

        public int Year { get; set; }

        public int Month {  get; set; }

        public MonthlyRecordStatus Status { get; set; } = MonthlyRecordStatus.Open;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LockedAt {  get; set; }

        public string? LockedByUserId { get; set; }

        public Employee Employee { get; set; } = null!;

        public School School { get; set; } = null!;
    }
}
