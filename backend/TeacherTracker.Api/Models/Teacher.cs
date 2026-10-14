namespace TeacherTracker.Api.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string OIB { get; set; } = string.Empty;
    }
}
