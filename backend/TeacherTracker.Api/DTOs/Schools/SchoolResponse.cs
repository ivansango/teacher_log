namespace TeacherTracker.Api.DTOs.Schools
{
    public class SchoolResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string OIB { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
