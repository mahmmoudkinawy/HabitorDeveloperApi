namespace Habitor.Api.Contracts.HabitTags;

public sealed record UpsertHabitTagsRequest
{
	public required List<string> TagIds { get; init; }
}
