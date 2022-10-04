using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

namespace LibYear;

public class Command : AsyncCommand<Settings>
{
	private readonly IAnsiConsole _console;

	public Command(IAnsiConsole console)
		=> _console = console;

	public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
	{
		try
		{
			return await _console.Status().StartAsync("Running...", Run(settings));
		}
		catch (Exception e)
		{
			_console.WriteException(e, ExceptionFormats.ShortenEverything);
			return 1;
		}
	}

	private Func<StatusContext, Task<int>> Run(Settings settings)
		=> async _ => await Factory.App(_console).Run(settings);
}