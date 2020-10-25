using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Models
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
