using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(45);

            builder.Property(e => e.Password).HasMaxLength(12);

            builder.Property(e => e.PhoneNumber).HasMaxLength(15);

            builder.HasOne(e => e.Role).WithMany(q => q.Users);
        }
    }
}
