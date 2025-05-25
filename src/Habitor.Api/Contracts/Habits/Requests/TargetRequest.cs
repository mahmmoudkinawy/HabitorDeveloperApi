namespace Habitor.Api.Contracts.Habits.Requests;

public sealed record TargetRequest
{
	public required int Value { get; init; }
	public required string Unit { get; init; }
}
