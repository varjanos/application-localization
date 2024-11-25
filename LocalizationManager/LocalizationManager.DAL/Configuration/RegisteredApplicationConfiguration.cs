using LocalizationManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalizationManager.DAL.Configuration;

internal class RegisteredApplicationConfiguration : IEntityTypeConfiguration<RegisteredApplication>
{
    public void Configure(EntityTypeBuilder<RegisteredApplication> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(x => x.AppId)
           .IsUnique();

        builder.Property(x => x.AppId).HasMaxLength(200);
        builder.Property(x => x.AppName).HasMaxLength(500);
    }
}
