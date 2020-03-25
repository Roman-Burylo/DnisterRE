using DAL.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class DnisterDbContext : DbContext
    {
        public DnisterDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> UserRoles { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());

        }
    }
}
