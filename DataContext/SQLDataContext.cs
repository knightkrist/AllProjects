using AllProjects.Models;
using Microsoft.EntityFrameworkCore;

namespace AllProjects.DataContext
{
    public class SQLDataContext:DbContext
    {
        public static IConfiguration Configuration { get; private set; }
        public SQLDataContext(DbContextOptions<SQLDataContext> options)
: base(options)
        { }
        public DbSet<Users> MyUser { get; set; }

        
    }
}
