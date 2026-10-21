namespace TeacherTracker.Api.Models;

public class MonthlyRecordVersion
{
    public int Id { get; set; }

    public int MonthlyRecordId { get; set; }

    public int VersionNumber { get; set; }

    public string SnapshotData { get; set; } = string.Empty;

    public int SchemaVersion { get; set; } = 1;

    public string ContentHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string CreatedByUserId { get; set; } = string.Empty;

    public MonthlyRecord MonthlyRecord { get; set; } = null!;
}