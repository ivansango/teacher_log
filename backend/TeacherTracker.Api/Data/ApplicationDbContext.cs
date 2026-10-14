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
    }
}
