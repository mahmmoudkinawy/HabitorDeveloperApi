using Habitor.Api;
using Habitor.Api.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;

namespace HabitorDeveloper.Tests.Integration;

public class HabitorApiFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
	private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().Build();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureLogging(logging => logging.ClearProviders());

		builder.ConfigureServices(services =>
		{
			services.RemoveAll<HabitorDbContext>();

			services.AddDbContext<HabitorDbContext>(options =>
			{
				options
					.UseNpgsql(
						_dbContainer.GetConnectionString(),
						dbOptions => dbOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Domain)
					)
					.UseSnakeCaseNamingConvention();
			});
		});
	}

	public async Task InitializeAsync()
	{
		await _dbContainer.StartAsync();

		using var scope = Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<HabitorDbContext>();
		await dbContext.Database.MigrateAsync();
	}

	public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();
}
