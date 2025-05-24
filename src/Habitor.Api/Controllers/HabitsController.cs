using Habitor.Api.Contracts.Habits;
using Habitor.Api.DbContexts;
using Habitor.Api.Extensions;
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
	public async Task<IActionResult> GetHabitByID([FromRoute] string id)
	{
		var habit = await _dbContext.Habits.Where(h => h.Id == id).Select(h => h.ToHabitResponse()).FirstOrDefaultAsync();

		if (habit is null)
		{
			return NotFound();
		}

		return Ok(habit);
	}
}
