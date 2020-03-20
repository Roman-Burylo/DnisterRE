using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");

            builder.HasKey(i=>i.Id);

            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.Property(i => i.Name).HasMaxLength(100);

            builder.Property(i => i.Description).HasMaxLength(8000);

        }
    }
}
