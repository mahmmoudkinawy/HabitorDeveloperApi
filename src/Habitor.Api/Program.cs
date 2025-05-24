using Habitor.Api.DbContexts;
using Habitor.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<HabitorDbContext>(options =>
	options
		.UseNpgsql(
			builder.Configuration.GetConnectionString("DefaultConnection"),
			dbOptions => dbOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Domain)
		)
		.UseSnakeCaseNamingConvention()
);

builder
	.Services.AddOpenTelemetry()
	.ConfigureResource(resource => resource.AddService(builder.Environment.ApplicationName))
	.WithTracing(tracing => tracing.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation().AddNpgsql())
	.WithMetrics(metrics => metrics.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation().AddRuntimeInstrumentation())
	.UseOtlpExporter();

builder.Logging.AddOpenTelemetry(options =>
{
	options.IncludeScopes = true;
	options.IncludeFormattedMessage = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
