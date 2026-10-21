namespace TeacherTracker.Api.Models;

public class DailyWorkEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid DailyRecordId { get; set; }

    public int WorkCategoryId { get; set; }

    public decimal Hours { get; set; }

    public string? Note { get; set; }

    public DailyRecord DailyRecord { get; set; } = null!;

    public WorkCategory WorkCategory { get; set; } = null!;
}