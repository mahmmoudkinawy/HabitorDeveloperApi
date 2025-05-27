using Habitor.Api.Contracts.Habits.Requests;
using Habitor.Api.Contracts.Habits.Responses;
using Habitor.Api.Entities;

namespace Habitor.Api.Extensions;

public static class HabitExtensions
{
	public static HabitEntity ToHabit(this CreateHabitRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);

		return new HabitEntity
		{
			Id = $"h_{Guid.CreateVersion7()}",
			Name = request.Name,
			Description = request.Description,
			Type = request.Type,
			Frequency = new Frequency { Type = request.Frequency.Type, TimesPerPeriod = request.Frequency.TimesPerPeriod },
			Target = new Target { Unit = request.Target.Unit, Value = request.Target.Value },
			Status = HabitStatus.Onging,
			IsArchived = false,
			EndDate = request.EndDate,
			Milestone = request.Milestone is not null ? new Milestone { Current = request.Milestone.Current, Target = 0 } : null,
			CreatedAtUtc = DateTime.UtcNow
		};
	}

	public static HabitEntity ToHabitEntity(this UpdateHabitRequest request, HabitEntity habit)
	{
		ArgumentNullException.ThrowIfNull(request);
		ArgumentNullException.ThrowIfNull(habit);

		return new HabitEntity
		{
			Id = habit.Id,
			Name = request.Name,
			Description = request.Description,
			Type = request.Type,
			Frequency = new Frequency { Type = request.Frequency.Type, TimesPerPeriod = request.Frequency.TimesPerPeriod },
			Target = new Target { Unit = request.Target.Unit, Value = request.Target.Value },
			Status = HabitStatus.Onging,
			IsArchived = false,
			EndDate = request.EndDate,
			Milestone = request.Milestone is not null ? new Milestone { Target = request.Milestone.Target } : new Milestone(),
			CreatedAtUtc = habit.CreatedAtUtc,
			UpdatedAtUtc = DateTime.UtcNow
		};
	}

	public static HabitResponse ToHabitResponse(this HabitEntity habit)
	{
		ArgumentNullException.ThrowIfNull(habit);

		return new HabitResponse
		{
			Id = habit.Id,
			Name = habit.Name,
			Description = habit.Description,
			Type = habit.Type,
			Frequency = new FrequencyResponse { TimesPerPeriod = habit.Frequency.TimesPerPeriod, Type = habit.Frequency.Type },
			IsArchived = habit.IsArchived,
			Status = habit.Status,
			Target = new TargetResponse { Unit = habit.Target.Unit, Value = habit.Target.Value },
			CreatedAtUtc = habit.CreatedAtUtc,
			EndDate = habit.EndDate,
			LastCompletedAtUtc = habit.LastCompletedAtUtc,
			Milestone = habit.Milestone == null ? null : new MilestoneResponse { Current = habit.Milestone.Current, Target = habit.Milestone.Target },
			UpdatedAtUtc = habit.UpdatedAtUtc
		};
	}
}
