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
                      .HasDefaultValue(MonthlyRecordStatus.Open);

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
        }
    }
}
