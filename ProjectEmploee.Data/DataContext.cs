using Microsoft.EntityFrameworkCore;
using ProjectEmploee.Core.Entities;

namespace ProjectEmploee.Data
{
    public class DataContext:DbContext
    {

        public DbSet<Attendance> AttendanceList { get; set; }
        public DbSet<Request> RequestList { get; set; }
        public DbSet<User> UserList { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=emploee-1");
        }
    }

}
