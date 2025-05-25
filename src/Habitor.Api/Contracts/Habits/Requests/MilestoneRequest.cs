namespace Habitor.Api.Contracts.Habits.Requests;

public sealed record MilestoneRequest
{
	public required int Target { get; init; }
	public required int Current { get; init; }
}
