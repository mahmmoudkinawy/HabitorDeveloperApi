namespace Habitor.Api.Contracts.Tags.Responses;

public sealed record TagsCollectionResponse
{
	public List<TagResponse> Data { get; init; }
}
