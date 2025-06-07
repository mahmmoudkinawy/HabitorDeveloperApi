using Habitor.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habitor.Api.DbContexts.Configurations;

public sealed class HabitTagEntityConfiguration : IEntityTypeConfiguration<HabitTagEntity>
{
	public void Configure(EntityTypeBuilder<HabitTagEntity> builder)
	{
		builder.HasKey(ht => new { ht.HabitId, ht.TagId });

		builder.HasOne<TagEntity>().WithMany().HasForeignKey(ht => ht.TagId);

		builder.HasOne<HabitEntity>().WithMany(h => h.HabitTags).HasForeignKey(ht => ht.HabitId);
	}
}
