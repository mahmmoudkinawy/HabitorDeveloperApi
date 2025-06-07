using Habitor.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Habitor.Api.DbContexts;

public sealed class HabitorDbContext(DbContextOptions<HabitorDbContext> options) : DbContext(options)
{
	public DbSet<HabitEntity> Habits { get; set; }
	public DbSet<TagEntity> Tags { get; set; }
	public DbSet<HabitTagEntity> HabitTags { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema(Schemas.Domain);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);

		base.OnModelCreating(modelBuilder);
	}
}
