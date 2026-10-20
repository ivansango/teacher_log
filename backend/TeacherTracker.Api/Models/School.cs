using System.ComponentModel.DataAnnotations;

namespace TeacherTracker.Api.Models
{
    public class School
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(11)]
        public string OIB { get; set; } = "";

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = "";

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = "";

        public bool IsActive { get; set; } = true;
    }
}
