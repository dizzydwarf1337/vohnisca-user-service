using api.Core.Configuration.Infrastructure;
using api.Core.Configuration.Middleware;
using api.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile(".env", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services
    .AddDatabase(builder.Configuration, builder.Environment)
    .AddRepositories()
    .AddApplicationServices()
    .AddHttpRpcClients(builder.Configuration)
    .AddInfrastructure()
    .AddAppServices()
    .AddCorsPolicy();

builder.Host
    .UseSerilogConsoleAndFile();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VohniscaDbContext>();
    db.Database.Migrate();
}

app.UseRouting();

app.UseJsonRpc();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();