using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduArticles.Models.Entities;

namespace EduArticles.DB.Configurations;

internal class ReplyConfiguration : IEntityTypeConfiguration<ReplyEntity>
{
    public void Configure(EntityTypeBuilder<ReplyEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content).IsRequired();
    }
}
