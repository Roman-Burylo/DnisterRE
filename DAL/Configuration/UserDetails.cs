using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    internal class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {
            builder.ToTable("UsersDetails");
            builder.Property(e => e.FirstName).HasMaxLength(32);
            builder.Property(e => e.LastName).HasMaxLength(32);            
        }
    }
}