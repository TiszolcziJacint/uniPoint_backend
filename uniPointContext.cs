using Microsoft.EntityFrameworkCore;
using uniPoint_backend.Models;

namespace uniPoint_backend
{
    public class uniPointContext : DbContext 
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        public uniPointContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        }
    }
}
