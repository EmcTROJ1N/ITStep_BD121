using Microsoft.EntityFrameworkCore;

namespace DBFirstCRUDRazorPage.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }

        public DbSet<Car> Cars { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }
    }
}
