using System.ComponentModel.DataAnnotations;
using TeacherTracker.Api.Enums;

namespace TeacherTracker.Api.Models
{
    public class AttendanceCode
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public AttendanceCategory Category { get; set; }

        public EmployeeType? EmployeeType { get; set; }

        public int? WorkCategoryId { get; set; }

        public decimal? HoursValue { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }

        public WorkCategory? WorkCategory { get; set; }
    }
}
