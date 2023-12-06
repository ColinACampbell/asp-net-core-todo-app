using Microsoft.EntityFrameworkCore;

namespace MyTodo.Models
{
    public class AppContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost:5432;Database=trained_to_go_db;Username=postgres;Password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(e=>e.Todos).WithOne(e=>e.User).HasForeignKey(e=>e.userId).HasPrincipalKey(e=>e.Id);
        }
    }
}

