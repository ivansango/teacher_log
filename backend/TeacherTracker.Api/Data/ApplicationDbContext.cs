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
        }
    }
}
