using Habitor.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habitor.Api.DbContexts.Configurations;

public sealed class HabitEntityConfiguration : IEntityTypeConfiguration<HabitEntity>
{
	public void Configure(EntityTypeBuilder<HabitEntity> builder)
	{
		builder.HasKey(h => h.Id);

		builder.Property(h => h.Id).HasMaxLength(500);

		builder.Property(h => h.Name).HasMaxLength(100);

		builder.Property(h => h.Description).HasMaxLength(500);

		builder.OwnsOne(h => h.Frequency);

		builder.OwnsOne(h => h.Target, targetBuilder => targetBuilder.Property(t => t.Unit).HasMaxLength(100));

		builder.OwnsOne(h => h.Milestone);
	}
}
