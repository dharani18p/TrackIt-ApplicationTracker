using Microsoft.EntityFrameworkCore;
using ApplicationTrackingSystem.Models;

namespace ApplicationTrackingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<JobRole> JobRoles { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
