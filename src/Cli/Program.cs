using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Terminal.Gui;
using TypeType.Cli.Extensions;
using TypeType.Cli.Commands;
using TypeType.Lib;
using TypeType.Lib.Data;
using TypeType.Tui;
using Velopack;
using TypeType.Tui.ViewModels;
using TypeType.Tui.Services;
using TypeType.Tui.Views;

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
                services.AddTransient<LoginView>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<GameView>();
                services.AddTransient<GameViewModel>();

                services.AddTransient<INavigationService, NavigationService>();
                services.AddSingleton<Func<Type, Window>>(serviceProvider => viewModelType => (Window)serviceProvider.GetRequiredService(viewModelType));
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
