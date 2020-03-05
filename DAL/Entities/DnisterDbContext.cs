using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class DnisterDbContext : DbContext
    {
        public DnisterDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
