using Habitor.Api.Contracts.HabitTags;
using Habitor.Api.DbContexts;
using Habitor.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Habitor.Api.Controllers;

[ApiController]
[Route("api/habits/{habitId}/tags")]
public sealed class HabitTagsController(HabitorDbContext dbContext) : ControllerBase
{
	private readonly HabitorDbContext _dbContext = dbContext;

	[HttpPut]
	public async Task<IActionResult> UpsertHabitTags([FromRoute] string habitId, [FromBody] UpsertHabitTagsRequest request)
	{
		var habit = await _dbContext.Habits.Include(h => h.HabitTags).FirstOrDefaultAsync(h => h.Id == habitId);

		if (habit is null)
		{
			return NotFound();
		}

		var currentTagIds = habit.HabitTags.Select(ht => ht.TagId).ToHashSet();

		if (currentTagIds.SetEquals(request.TagIds))
		{
			return NoContent();
		}

		var existingTagIds = await _dbContext.Tags.Where(t => request.TagIds.Contains(t.Id)).Select(t => t.Id).ToListAsync();

		if (existingTagIds.Count != request.TagIds.Count)
		{
			return BadRequest("Some tags do not exist.");
		}

		habit.HabitTags.RemoveAll(ht => !request.TagIds.Contains(ht.TagId));

		var tagIdsToAdd = request.TagIds.Except(currentTagIds);

		habit.HabitTags.AddRange(
			tagIdsToAdd.Select(tagId => new HabitTagEntity
			{
				HabitId = habitId,
				TagId = tagId,
				CreatedAtUtc = DateTime.UtcNow
			})
		);

		await _dbContext.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteHabitTag([FromRoute] string habitId, [FromRoute] string id)
	{
		var habitTag = await _dbContext.HabitTags.SingleOrDefaultAsync(ht => ht.HabitId == habitId && ht.TagId == id);

		if (habitTag is null)
		{
			return NotFound();
		}

		_dbContext.HabitTags.Remove(habitTag);

		await _dbContext.SaveChangesAsync();

		return NoContent();
	}
}
