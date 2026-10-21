namespace TeacherTracker.Api.Models;

public class AuditLog
{
    public long Id { get; set; }

    public int SchoolId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string EntityType { get; set; } = string.Empty;

    public string EntityId { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public Guid CorrelationId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public School School { get; set; } = null!;
}