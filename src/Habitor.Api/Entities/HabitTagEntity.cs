namespace Habitor.Api.Entities;

public sealed class HabitTagEntity
{
	public required string HabitId { get; set; }
	public required string TagId { get; set; }
	public required DateTime CreatedAtUtc { get; set; }
}
