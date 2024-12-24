using Microsoft.Extensions.Logging;
using System.CommandLine.Invocation;
using Terminal.Gui;
using TypeType.Lib.Data;
using TypeType.Tui.Views;

namespace TypeType.Cli.Commands;

internal sealed class SelectModeCommand : System.CommandLine.Command
{
    public SelectModeCommand() : base("sample", "A  sample command")
    {
        AddOptions(this);
    }

    public static void AddOptions(System.CommandLine.Command command) { }

    new public class Handler(LoginView view, ILogger<SelectModeCommand> logger, DbContext db) : ICommandHandler
    {
        private readonly DbContext db = db;

        public int Invoke(InvocationContext context) => InvokeAsync(context).Result;

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            Application.Init();
            Application.Run(view);
            Application.Top?.Dispose();
            Application.Shutdown();
            return -0;
        }
    }
}
