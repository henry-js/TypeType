using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Globalization;
using TypeType.Lib.Data;

namespace TypeType.Cli.Commands;

internal sealed class SampleCommand : Command
{
    public SampleCommand() : base("sample", "A  sample command")
    {
        AddOptions(this);
    }

    public static void AddOptions(Command command) { }

    new public class Handler(IAnsiConsole console, ILogger<SampleCommand> logger, DbContext db) : ICommandHandler
    {
        private readonly DbContext db = db;

        public int Invoke(InvocationContext context) => InvokeAsync(context).Result;

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };
            return 0;
        }
    }
}
