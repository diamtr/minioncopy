using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.Composition;
using ToolBox.Shared;

namespace MinionCopy
{
  [Export(typeof(IConsoleTool))]
  public class ConsoleToolAdapter : IConsoleTool
  {
    public CommandLineApplication Command { get; private set; }

    public ConsoleToolAdapter()
    {
      this.Command = this.InitCommand();
    }

    private CommandLineApplication InitCommand()
    {
      var app = new CommandLineApplication() { Name = "mcopy", Description = "'Minion copy' tool" };
      app.HelpOption(inherited: true);
      app.OnExecute(() => { app.ShowHelp(); return 1; });
      app.AddSubcommand(new CopyFileCommand());
      app.AddSubcommand(new CopyDirectoryCommand());
      app.AddSubcommand(new CopyManyCommand());
      return app;
    }
  }
}
