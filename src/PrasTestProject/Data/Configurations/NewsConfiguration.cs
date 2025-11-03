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

            builder.Property(x => x.TitleRu).HasMaxLength(200).IsRequired();
            builder.Property(x => x.TitleEn).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SubtitleRu).HasMaxLength(300).IsRequired();
            builder.Property(x => x.SubtitleEn).HasMaxLength(300).IsRequired();
            builder.Property(x => x.BodyRu).IsRequired();
            builder.Property(x => x.BodyEn).IsRequired();
            builder.HasIndex(x => new { x.IsPublished, x.CreatedAtUtc });
        }
    }
}
}
