using Microsoft.EntityFrameworkCore;
using WebShell.Domain;

namespace WebShell.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<ShellRequest> Requests { get; set; }
        public DataContext()
        {
            Database.EnsureCreated();
        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        { 
        
        }

    }
}
