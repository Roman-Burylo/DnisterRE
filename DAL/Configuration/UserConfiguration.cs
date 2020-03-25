using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Email).HasMaxLength(256);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne(d => d.Details).WithOne(u => u.User).HasForeignKey<UserDetails>(x => x.UserDetailsId);

            builder.HasOne(d => d.Confirmation).WithOne(u => u.User).HasForeignKey<UserConfirmation>(x => x.Id);
        }
    }
}