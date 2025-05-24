namespace Habitor.Api.Contracts.Habits;

public sealed record TargetResponse
{
	public required int Value { get; init; }
	public required int Unit { get; init; }
}
