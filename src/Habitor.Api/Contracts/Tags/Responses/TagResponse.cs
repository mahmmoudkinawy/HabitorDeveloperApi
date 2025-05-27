namespace Habitor.Api.Contracts.Tags.Responses;

public sealed record TagResponse
{
	public required string Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public required DateTime CreatedAtUtc { get; set; }
	public DateTime? UpdatedAtUtc { get; set; }
}
