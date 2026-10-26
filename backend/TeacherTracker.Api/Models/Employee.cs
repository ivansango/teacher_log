using System.ComponentModel.DataAnnotations;

namespace TeacherTracker.Api.Models
{
    public class Employee
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public string? UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
