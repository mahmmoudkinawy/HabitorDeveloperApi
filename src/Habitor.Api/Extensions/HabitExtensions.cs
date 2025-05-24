using Habitor.Api.Contracts.Habits;
using Habitor.Api.Entities;

namespace Habitor.Api.Extensions;

public static class HabitExtensions
{
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
