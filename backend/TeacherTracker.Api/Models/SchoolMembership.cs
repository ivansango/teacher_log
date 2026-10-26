namespace TeacherTracker.Api.Models;

public class SchoolMembership
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public int SchoolId { get; set; }

    public string Role { get; set; } = "SchoolAdmin";

    public bool IsActive { get; set; } = true;

    public ApplicationUser User { get; set; } = null!;

    public School School { get; set; } = null!;
}