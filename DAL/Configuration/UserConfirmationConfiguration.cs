using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    internal class UserConfirmationConfiguration : IEntityTypeConfiguration<UserConfirmation>
    {
        public void Configure(EntityTypeBuilder<UserConfirmation> builder)
        {
            builder.ToTable("UsersConfirmations");
        }
    }
}