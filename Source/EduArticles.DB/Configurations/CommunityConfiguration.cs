using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduArticles.Models.Entities;

namespace EduArticles.DB.Configurations;

internal class CommunityConfiguration : IEntityTypeConfiguration<CommunityEntity>
{
    public void Configure(EntityTypeBuilder<CommunityEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Image).IsRequired();

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Community)
            .HasForeignKey(x => x.CommunityId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
