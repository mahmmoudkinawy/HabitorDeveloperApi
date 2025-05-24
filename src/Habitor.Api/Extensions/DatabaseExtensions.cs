using Habitor.Api.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Habitor.Api.Extensions;

public static class DatabaseExtensions
{
	public static async Task ApplyMigrationsAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<HabitorDbContext>();

		try
		{
			await dbContext.Database.MigrateAsync();

			app.Logger.LogInformation("Database migrations applied successfully.");
		}
		catch (Exception ex)
		{
			app.Logger.LogError(ex, "An error occurred while applying database migrations.");
			throw;
		}
	}
}
