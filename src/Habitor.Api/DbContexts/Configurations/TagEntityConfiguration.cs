using Habitor.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habitor.Api.DbContexts.Configurations;

public sealed class TagEntityConfiguration : IEntityTypeConfiguration<TagEntity>
{
	public void Configure(EntityTypeBuilder<TagEntity> builder)
	{
		builder.HasKey(t => t.Id);

		builder.Property(t => t.Id).HasMaxLength(500);

		builder.Property(t => t.Name).IsRequired().HasMaxLength(50);

		builder.Property(t => t.Description).HasMaxLength(500);

		builder.HasIndex(t => new { t.Name }).IsUnique();
	}
}
