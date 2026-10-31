using System.ComponentModel.DataAnnotations;

namespace TeacherTracker.Api.DTOs.Schools
{
    public class UpdateSchoolRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{11}$")]
        public string OIB {  get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address {  get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string City { get; set; } = string.Empty;

    }
}
