using System.ComponentModel.DataAnnotations;
using TeacherTracker.Api.Enums;

namespace TeacherTracker.Api.Models
{
    public class DailyRecord
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public int MonthlyRecordId { get; set; }

        public DateOnly WorkDate { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? Note { get; set; }

        public string CreatedByUserId { get; set; } = string.Empty;

        public string? UpdatedByUserId { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public MonthlyRecord MonthlyRecord { get; set; } = null!;

        
    }
}
