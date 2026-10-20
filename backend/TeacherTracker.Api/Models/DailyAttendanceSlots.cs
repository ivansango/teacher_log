using TeacherTracker.Api.Enums;

namespace TeacherTracker.Api.Models;

public class DailyAttendanceSlot
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid DailyRecordId { get; set; }

    public WorkShift Shift { get; set; }

    public int SlotNumber { get; set; }

    public int? AttendanceCodeId { get; set; }

    public string? Note { get; set; }

    public DailyRecord DailyRecord { get; set; } = null!;

    public AttendanceCode? AttendanceCode { get; set; }
}