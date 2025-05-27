using Habitor.Api.Contracts.Habits.Requests;
using Habitor.Api.Contracts.Habits.Responses;
using Habitor.Api.DbContexts;
using Habitor.Api.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Habitor.Api.Controllers;

[ApiController]
[Route("api/habits")]
public sealed class HabitsController(HabitorDbContext dbContext) : ControllerBase
{
	private readonly HabitorDbContext _dbContext = dbContext;

	[HttpGet]
	public async Task<IActionResult> GetHabits()
	{
		var habits = await _dbContext.Habits.Select(h => h.ToHabitResponse()).ToListAsync();

		var habtisCollectionResponse = new HabitCollectionResponse { Data = habits };

		return Ok(habtisCollectionResponse);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetHabitById([FromRoute] string id)
	{
		var habit = await _dbContext.Habits.Where(h => h.Id == id).Select(h => h.ToHabitResponse()).FirstOrDefaultAsync();

		if (habit is null)
		{
			return NotFound();
		}

		return Ok(habit);
	}

	[HttpPost]
	public async Task<IActionResult> CreateHabit([FromBody] CreateHabitRequest request)
	{
		var habit = request.ToHabit();

		_dbContext.Habits.Add(habit);
		await _dbContext.SaveChangesAsync();

		var habitResponse = habit.ToHabitResponse();

		return CreatedAtAction(nameof(GetHabitById), new { id = habitResponse.Id }, habitResponse);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateHabit([FromRoute] string id, [FromBody] UpdateHabitRequest request)
	{
		var habit = await _dbContext.Habits.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

		if (habit is null)
		{
			return NotFound();
		}

		var habitToUpdate = request.ToHabitEntity(habit);

		_dbContext.Entry(habitToUpdate).State = EntityState.Modified;
		await _dbContext.SaveChangesAsync();

		return NoContent();
	}

	[HttpPatch("{id}")]
	public async Task<IActionResult> PatchHabit([FromRoute] string id, JsonPatchDocument<HabitResponse> jsonPatchDocument)
	{
		var habit = await _dbContext.Habits.FirstOrDefaultAsync(h => h.Id == id);

		if (habit is null)
		{
			return NotFound();
		}

		var habitResponse = habit.ToHabitResponse();

		jsonPatchDocument.ApplyTo(habitResponse, ModelState);

		if (!TryValidateModel(habitResponse))
		{
			return ValidationProblem(ModelState);
		}

		habit.Name = habitResponse.Name;
		habit.Description = habitResponse.Description;
		habit.UpdatedAtUtc = DateTime.UtcNow;

		await _dbContext.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteHabit([FromRoute] string id)
	{
		var habit = await _dbContext.Habits.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

		if (habit is null)
		{
			return NotFound();
		}

		_dbContext.Habits.Remove(habit);
		await _dbContext.SaveChangesAsync();

		return NoContent();
	}
}
