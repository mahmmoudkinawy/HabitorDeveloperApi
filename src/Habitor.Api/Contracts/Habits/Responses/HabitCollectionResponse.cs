namespace Habitor.Api.Contracts.Habits.Responses;

public sealed record HabitCollectionResponse
{
	public List<HabitResponse> Data { get; init; }
}
