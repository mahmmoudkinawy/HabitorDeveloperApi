﻿using Habitor.Api.Entities;

namespace Habitor.Api.Contracts.Habits.Responses;

public sealed record FrequencyResponse
{
	public required FrequencyType Type { get; init; }
	public required int TimesPerPeriod { get; init; }
}
