using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace HabitorDeveloper.Tests.Integration.Controllers.HabitsController;

public class GetHabitsControllerTests : IClassFixture<HabitorApiFactory>
{
	private readonly HabitorApiFactory _apiFactory;

	public GetHabitsControllerTests(HabitorApiFactory apiFactory)
	{
		_apiFactory = apiFactory;
	}

	[Fact]
	public async Task Get_ReturnsNotFound_WhenHabitDoesNotExist()
	{
		// Arrange
		var client = _apiFactory.CreateClient();

		// Act
		var response = await client.GetAsync($"api/habits/h_{Guid.CreateVersion7()}");

		// Assert
		var result = await response.Content.ReadFromJsonAsync<ProblemDetails>();

		result!.Title.ShouldBe("Not Found");
		result!.Status.ShouldBe(StatusCodes.Status200OK);
	}
}
