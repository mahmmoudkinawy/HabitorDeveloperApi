namespace Habitor.Api.Contracts.Habits;

public sealed record MilestoneResponse
{
	public required int Target { get; init; }
	public required int Current { get; init; }
}
