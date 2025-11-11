using Microsoft.EntityFrameworkCore;
using NotificationCenter.Models;

namespace NotificationCenter.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<NotificationTask> NotificationTasks { get; set; }
    }
}