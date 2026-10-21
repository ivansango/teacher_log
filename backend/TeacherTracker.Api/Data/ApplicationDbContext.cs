using Microsoft.EntityFrameworkCore;
using TeacherTracker.Api.Enums;
using TeacherTracker.Api.Models;

namespace TeacherTracker.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options ) : base( options ) 
        {
        }

        public DbSet<Teacher> Teachers => Set<Teacher>();

        public DbSet<School> Schools => Set<School>();

        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<EmploymentRecord> EmploymentRecords => Set<EmploymentRecord>();

        public DbSet<WorkCategory> WorkCategories => Set<WorkCategory>();

        public DbSet<AttendanceCode> AttendanceCodes => Set<AttendanceCode>();

        public DbSet<MonthlyRecord> MonthlyRecords => Set<MonthlyRecord>();

        public DbSet<DailyRecord> DailyRecords => Set<DailyRecord>();

        public DbSet<DailyAttendanceSlot> DailyAttendanceSlots => Set<DailyAttendanceSlot>();

        public DbSet<DailyWorkEntry> DailyWorkEntries => Set<DailyWorkEntry>();

        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        public DbSet<MonthlyRecordVersion> MonthlyRecordVersions => Set<MonthlyRecordVersion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(s => s.OIB)
                    .IsUnique();

                entity.Property(s => s.IsActive)
                    .HasDefaultValue(true);

            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.HasIndex(e => e.UserId)
                    .IsUnique();

            });


            modelBuilder.Entity<EmploymentRecord>(entity =>
            {
                entity.HasOne(er => er.Employee)
                      .WithMany()
                      .HasForeignKey(er => er.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(er => er.School)
                      .WithMany()
                      .HasForeignKey(er => er.SchoolId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(er => er.WeeklyContractHours)
                      .HasPrecision(5, 2);

                entity.Property(er => er.WeeklyAssignedHours)
                      .HasPrecision(5, 2);

                entity.Property(er => er.EmployeeType)
                       .HasConversion<string>()
                       .HasMaxLength(50);

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "ck_employment_records_valid_dates",
                        "valid_to IS NULL OR valid_to >= valid_from"
                        );
                });
            });

            modelBuilder.Entity<WorkCategory>(entity =>
            {
                entity.HasIndex(wc => wc.Code)
                       .IsUnique();

                entity.Property(wc => wc.EmployeeType)
                       .HasConversion<string>()
                       .HasMaxLength(50);

                entity.Property(wc => wc.IsActive)
                       .HasDefaultValue(true);
            });

            modelBuilder.Entity<AttendanceCode>(entity =>
            {
                entity.HasIndex(ac => ac.Code)
                      .IsUnique();

                entity.Property(ac => ac.Category)
                      .HasConversion<string>()
                      .HasMaxLength(30);

                entity.Property(ac => ac.EmployeeType)
                      .HasConversion<string>()
                      .HasMaxLength(50);

                entity.HasOne(ac => ac.WorkCategory)
                      .WithMany()
                      .HasForeignKey(ac => ac.WorkCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(ac => ac.HoursValue)
                      .HasPrecision(5, 2);
                entity.Property(ac => ac.IsActive)
                      .HasDefaultValue(true);
            });

            modelBuilder.Entity<MonthlyRecord>(entity =>
            {
                entity.HasIndex(mr => new
                {
                    mr.EmployeeId,
                    mr.SchoolId,
                    mr.Year,
                    mr.Month
                }).IsUnique();

                entity.HasOne(mr => mr.Employee)
                      .WithMany()
                      .HasForeignKey(mr => mr.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(mr => mr.School)
                      .WithMany()
                      .HasForeignKey(mr => mr.SchoolId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(mr => mr.Status)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(mr => mr.CreatedAt)
                      .HasColumnType("timestamp with time zone");

                entity.Property(mr => mr.LockedAt)
                      .HasColumnType("timestamp with time zone");

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "ck_monthly_records_month",
                        "month BETWEEN 1 AND 12");
                });
            });

            modelBuilder.Entity<DailyRecord>(entity =>
            {
                entity.HasKey(dr => dr.Id);

                entity.HasOne(dr => dr.MonthlyRecord)
                      .WithMany()
                      .HasForeignKey(dr => dr.MonthlyRecordId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(dr => new
                {
                    dr.MonthlyRecordId,
                    dr.WorkDate
                }).IsUnique();

                entity.Property(dr => dr.CreatedAt)
                      .HasColumnType("timestamp with time zone");

                entity.Property(dr => dr.UpdatedAt)
                      .HasColumnType("timestamp with time zone");
            });

            modelBuilder.Entity<DailyAttendanceSlot>(entity =>
            {
                entity.HasKey(das => das.Id);

                entity.HasOne(das => das.DailyRecord)
                      .WithMany()
                      .HasForeignKey(das => das.DailyRecordId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(das => das.AttendanceCode)
                      .WithMany()
                      .HasForeignKey(das => das.AttendanceCodeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(das => new
                {
                    das.DailyRecordId,
                    das.Shift,
                    das.SlotNumber
                }).IsUnique();

                entity.Property(das => das.Shift)
                      .HasConversion<string>()
                      .HasMaxLength(20);
            });

            modelBuilder.Entity<DailyWorkEntry>(entity =>
            {
                entity.HasKey(dwe => dwe.Id);

                entity.HasOne(dwe => dwe.DailyRecord)
                      .WithMany()
                      .HasForeignKey(dwe => dwe.DailyRecordId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(dwe => dwe.WorkCategory)
                      .WithMany()
                      .HasForeignKey(dwe => dwe.WorkCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(dwe => new
                {
                    dwe.DailyRecordId,
                    dwe.WorkCategoryId
                }).IsUnique();

                entity.Property(dwe => dwe.Hours)
                      .HasPrecision(5, 2);
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasOne(a => a.School)
                      .WithMany()
                      .HasForeignKey(a => a.SchoolId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.EntityType)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(a => a.EntityId)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(a => a.Action)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(a => a.UserId)
                      .IsRequired();

                entity.Property(a => a.OldValues)
                      .HasColumnType("jsonb");

                entity.Property(a => a.NewValues)
                      .HasColumnType("jsonb");

                entity.Property(a => a.CreatedAt)
                      .HasColumnType("timestamp with time zone");

                entity.HasIndex(a => new
                {
                    a.SchoolId,
                    a.CreatedAt
                });

                entity.HasIndex(a => a.CorrelationId);
            });

            modelBuilder.Entity<MonthlyRecordVersion>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.HasOne(v => v.MonthlyRecord)
                      .WithMany()
                      .HasForeignKey(v => v.MonthlyRecordId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(v => new
                {
                    v.MonthlyRecordId,
                    v.VersionNumber
                }).IsUnique();

                entity.Property(v => v.SnapshotData)
                      .HasColumnType("jsonb")
                      .IsRequired();

                entity.Property(v => v.SchemaVersion)
                      .HasDefaultValue(1);

                entity.Property(v => v.ContentHash)
                      .HasMaxLength(64)
                      .IsRequired();

                entity.Property(v => v.CreatedAt)
                      .HasColumnType("timestamp with time zone");

                entity.Property(v => v.CreatedByUserId)
                      .IsRequired();
            });
        }
    }
}
