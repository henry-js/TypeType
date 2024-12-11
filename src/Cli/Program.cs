using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using TypeType.Cli.Extensions;
using TypeType.Cli.Commands;
using Velopack;
using TypeType.Lib;
using TypeType.Lib.Data;
using CommunityToolkitExample;

VelopackApp.Build()
    .WithFirstRun(_ =>
    {
    })
    .Run();

var assemblyDir = Path.GetDirectoryName(AppContext.BaseDirectory);
var dbFile = Path.Combine(assemblyDir!, "typetype.db");
File.Move(dbFile, Path.Combine(BaseDirectories.DataDir, "typetype.db"), true);
var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Debug()
            // .WriteTo.File("logs/startup_.log",
            // outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] {SourceContext}: {Message:lj}{NewLine}{Exception}",
            // rollingInterval: RollingInterval.Day
            // )
            // .Enrich.WithProperty("Application Name", "<APP NAME>");
            .WriteTo.Console();
Log.Logger = loggerConfiguration.CreateBootstrapLogger();


var rootCommand = new RootCommand("root");
rootCommand.AddCommand(new SelectModeCommand());


var cmdLine = new CommandLineBuilder(rootCommand)
    .UseHost(_ => Host.CreateDefaultBuilder(args), builder =>
    {
        builder.ConfigureAppConfiguration(config =>
        {
        })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton(_ => AnsiConsole.Console);
                services.AddTypeTypeDb(LiteDbOptions.ConnectionString);
                services.AddTransient<GameView>();
                services.AddTransient<GameViewModel>();
                services.AddTransient<GameView>();
                services.AddTransient<GameViewModel>();
                services.AddTransient<INavigationService, NavigationService>();
            })
            .UseProjectCommandHandlers()
            .UseSerilog((context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));
    })
    .UseDefaults()
    // .UseExceptionHandler((ex, context) =>
    // {
    //     AnsiConsole.WriteException(ex, ExceptionFormats.Default);
    //     Log.Fatal(ex, "Application terminated unexpectedly");
    // })
    .Build();

int result = await cmdLine.InvokeAsync(args);

return result;
