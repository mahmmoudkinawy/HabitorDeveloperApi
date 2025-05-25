using Habitor.Api.Entities;

namespace Habitor.Api.Contracts.Habits.Requests;

public sealed record FrequencyRequest
{
	public required FrequencyType Type { get; init; }
	public required int TimesPerPeriod { get; init; }
}
