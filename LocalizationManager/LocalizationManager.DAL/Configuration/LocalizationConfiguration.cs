using LocalizationManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalizationManager.DAL.Configuration;

internal class LocalizationConfiguration : IEntityTypeConfiguration<LocalizationValue>
{
    public void Configure(EntityTypeBuilder<LocalizationValue> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(x => new { x.AppId, x.LanguageCode, x.Key })
            .IsUnique();

        builder.Property(x => x.AppId).HasMaxLength(200);
        builder.Property(x => x.Key).HasMaxLength(500);
        builder.Property(x => x.LanguageCode).HasMaxLength(100);
    }
}
