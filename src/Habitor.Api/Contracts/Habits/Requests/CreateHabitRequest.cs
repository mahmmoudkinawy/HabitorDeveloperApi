using Habitor.Api.Entities;

namespace Habitor.Api.Contracts.Habits.Requests;

public sealed record CreateHabitRequest
{
	public required string Name { get; init; }
	public string? Description { get; init; }
	public required HabitType Type { get; init; }
	public required FrequencyRequest Frequency { get; init; }
	public required TargetRequest Target { get; init; }
	public DateOnly? EndDate { get; init; }
	public MilestoneRequest? Milestone { get; init; }
}
