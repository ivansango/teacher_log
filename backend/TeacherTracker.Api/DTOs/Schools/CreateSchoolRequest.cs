using System.ComponentModel.DataAnnotations;

namespace TeacherTracker.Api.DTOs.Schools
{
    public class CreateSchoolRequest
    {

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "OIB must be exactly 11 digits.")]
        public string OIB { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

    }
}
