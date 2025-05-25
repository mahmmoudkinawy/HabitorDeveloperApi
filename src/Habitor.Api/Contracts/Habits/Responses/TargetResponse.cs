namespace Habitor.Api.Contracts.Habits.Responses;

public sealed record TargetResponse
{
	public required int Value { get; init; }
	public required string Unit { get; init; }
}
