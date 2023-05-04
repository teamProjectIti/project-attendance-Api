using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model.Attend;
using WebApplication1.Model.Meetings;
using WebApplication1.Model.MeetingUser;
using WebApplication1.Model.User;

namespace WebApplication1.Connection
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ManageUser> ManageUsers { get; set; }
        public DbSet<MeetingUsers> MeetingUsers { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your entity relationships and properties here
        }
    }
}
