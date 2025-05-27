using Habitor.Api.Contracts.Tags.Requests;
using Habitor.Api.Contracts.Tags.Responses;
using Habitor.Api.Entities;

namespace Habitor.Api.Extensions;

public static class TagExtensions
{
	public static TagResponse ToTagResponse(this TagEntity tag)
	{
		ArgumentNullException.ThrowIfNull(tag);

		return new TagResponse
		{
			Id = tag.Id,
			Name = tag.Name,
			Description = tag.Description,
			UpdatedAtUtc = tag.UpdatedAtUtc,
			CreatedAtUtc = tag.CreatedAtUtc
		};
	}

	public static TagEntity ToTagEntity(this CreateTagRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);

		return new TagEntity
		{
			Id = $"t_{Guid.CreateVersion7()}",
			Name = request.Name,
			Description = request.Description,
			CreatedAtUtc = DateTime.UtcNow
		};
	}

	public static TagEntity ToTagEntity(this UpdateTagRequest request, TagEntity tag)
	{
		ArgumentNullException.ThrowIfNull(request);
		ArgumentNullException.ThrowIfNull(tag);

		return new TagEntity
		{
			Id = tag.Id,
			Name = request.Name,
			Description = request.Description,
			UpdatedAtUtc = DateTime.UtcNow,
			CreatedAtUtc = tag.CreatedAtUtc
		};
	}
}
