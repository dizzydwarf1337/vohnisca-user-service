using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace api.Core.Configuration.Infrastructure;

public static class SerilogConfig
{
    public static IHostBuilder UseSerilogConsoleAndFile(this IHostBuilder hostBuilder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.IdentityModel", LogEventLevel.Debug)
            .WriteTo.Console(
                theme: AnsiConsoleTheme.Code,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
            )
            .WriteTo.File(
                path: "logs/log.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();

        hostBuilder.UseSerilog();

        return hostBuilder;
    }
}