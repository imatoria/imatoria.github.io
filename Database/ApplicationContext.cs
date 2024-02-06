using Microsoft.EntityFrameworkCore;

namespace PraveenMatoria.Database
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { 
        
        }

        public DbSet<Todo> Todos { get; set; } = null!;
    }
}
