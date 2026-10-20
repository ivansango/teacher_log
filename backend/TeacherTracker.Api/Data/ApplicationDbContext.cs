using Microsoft.EntityFrameworkCore;
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
        }
    }
}
