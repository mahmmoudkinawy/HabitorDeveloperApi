namespace Habitor.Api.Contracts.Tags.Requests;

public sealed record CreateTagRequest
{
	public required string Name { get; set; }
	public string? Description { get; set; }
}
