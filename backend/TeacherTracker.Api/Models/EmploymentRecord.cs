using System.ComponentModel.DataAnnotations;
using TeacherTracker.Api.Enums;

namespace TeacherTracker.Api.Models
{
    public class EmploymentRecord
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int SchoolId { get; set; }

        [Required]
        [MaxLength(150)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        public EmployeeType EmployeeType { get; set; } 

        public decimal WeeklyContractHours { get; set; }

        public decimal? WeeklyAssignedHours { get; set; } = null;

        public DateOnly ValidFrom { get; set; }

        public DateOnly? ValidTo { get; set; }

        public Employee Employee { get; set; } = null!;

        public School School { get; set; } = null!;

    }
}
