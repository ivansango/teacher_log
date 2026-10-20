using System.ComponentModel.DataAnnotations;
using TeacherTracker.Api.Enums;

namespace TeacherTracker.Api.Models
{
    public class WorkCategory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public EmployeeType? EmployeeType { get; set; }

        [Required]
        [MaxLength(30)]
        public string Unit { get; set; } = "hours";

        public bool IsActive { get; set; } = true;

        public int SortOrder { get; set; }
    }
}
