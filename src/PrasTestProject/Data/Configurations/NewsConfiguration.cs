using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrasTestProject.Data.Entities;

namespace PrasTestProject.Data.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder
                .ToTable("news")
                .HasKey(k => k.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Subtitle).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Body).IsRequired();

            builder.HasIndex(x => x.CreatedAtUtc);
        }
    }
}

