using Habitor.Api.Contracts.Tags.Requests;
using Habitor.Api.Contracts.Tags.Responses;
using Habitor.Api.DbContexts;
using Habitor.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Habitor.Api.Controllers;

[ApiController]
[Route("api/tags")]
public sealed class TagsController(HabitorDbContext dbContext) : ControllerBase
{
	private readonly HabitorDbContext _dbContext = dbContext;

	[HttpGet]
	public async Task<IActionResult> GetTags()
	{
		var tags = await _dbContext.Tags.Select(t => t.ToTagResponse()).ToListAsync();

		var tagsCollectionResponse = new TagsCollectionResponse { Data = tags };

		return Ok(tagsCollectionResponse);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetTagById([FromRoute] string id)
	{
		var tag = await _dbContext.Tags.Where(h => h.Id == id).Select(t => t.ToTagResponse()).FirstOrDefaultAsync();

		if (tag is null)
		{
			return NotFound();
		}

		return Ok(tag);
	}

	[HttpPost]
	public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest request)
	{
		var tag = request.ToTagEntity();

		if (await _dbContext.Tags.AnyAsync(t => t.Name == tag.Name))
		{
			return Conflict($"The tag name '{tag.Name}' already exists");
		}

		_dbContext.Tags.Add(tag);
		await _dbContext.SaveChangesAsync();

		var tagResponse = tag.ToTagResponse();

		return CreatedAtAction(nameof(GetTagById), new { id = tagResponse.Id }, tagResponse);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateTag([FromRoute] string id, [FromBody] UpdateTagRequest request)
	{
		var tag = await _dbContext.Tags.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

		if (tag is null)
		{
			return NotFound();
		}

		var tagToUpdate = request.ToTagEntity(tag);

		_dbContext.Entry(tagToUpdate).State = EntityState.Modified;
		await _dbContext.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteTag([FromRoute] string id)
	{
		var tag = await _dbContext.Tags.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

		if (tag is null)
		{
			return NotFound();
		}

		_dbContext.Tags.Remove(tag);
		await _dbContext.SaveChangesAsync();

		return NoContent();
	}
}
