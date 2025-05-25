using Habitor.Api.Entities;

namespace Habitor.Api.Contracts.Habits.Responses;

public sealed record HabitResponse
{
	public required string Id { get; init; }
	public required string Name { get; init; }
	public string? Description { get; init; }
	public required HabitType Type { get; init; }
	public required FrequencyResponse Frequency { get; init; }
	public required TargetResponse Target { get; init; }
	public required HabitStatus Status { get; init; }
	public required bool IsArchived { get; init; }
	public DateOnly? EndDate { get; init; }
	public MilestoneResponse? Milestone { get; init; }
	public DateTime CreatedAtUtc { get; init; }
	public DateTime? UpdatedAtUtc { get; init; }
	public DateTime? LastCompletedAtUtc { get; init; }
}
