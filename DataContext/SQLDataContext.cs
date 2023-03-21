using AllProjects.Models;
using Microsoft.EntityFrameworkCore;

namespace AllProjects.DataContext
{
    public class SQLDataContext : DbContext
    {
        public static IConfiguration Configuration { get; private set; }
        public SQLDataContext(DbContextOptions<SQLDataContext> options) : base(options) { }
        public DbSet<Users> MyUser { get; set; }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<OrderItem> orderItems => Set<OrderItem>();
        public DbSet<Order> orders => Set<Order>();
        public DbSet<Customer> customers => Set<Customer>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>(builder => builder.HasKey(x => x.Id));
        }

    }
}
