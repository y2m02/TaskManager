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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //"TaskManagerConnection": "Server = sql9.freemysqlhosting.net; Database = DBTaskManager; Trusted_Connection = False; User Id = sql9373616; Password = t4B9Uk2w8h;"

        //    optionsBuilder.UseMySQL(
        //        "server = sql9.freemysqlhosting.net;" +
        //        "database = DBTaskManager;" +
        //        "user = sql9373616;" +
        //        "password = t4B9Uk2w8h"
        //    );
        //}
    }
}
