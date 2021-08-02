using McMaster.Extensions.CommandLineUtils;

namespace MinionCopy
{
  public class CopyDirectoryCommand : CommandLineApplication
  {
    CommandArgument<string> source;
    CommandArgument<string> destination;
    CommandOption<bool> recursive;
    CommandOption<bool> replace;
    CommandOption<string> renameTo;

    public CopyDirectoryCommand()
    {
      this.Name = "dir";
      this.Description = "Copy directory";
      this.source = this.Argument<string>("source", "Source directory path to copy.").IsRequired();
      this.destination = this.Argument<string>("destination", "Destination path to copy").IsRequired();
      this.recursive = this.Option<bool>("--recursive", "Copy directory with all content", CommandOptionType.NoValue);
      this.replace = this.Option<bool>("--replace", "Replace directory if it already exists in destination.", CommandOptionType.NoValue);
      this.renameTo = this.Option<string>("--rename", "Rename directory in destination to specified name while copying.", CommandOptionType.SingleValue);
      this.OnExecute(this.Execute);
    }

    public int Execute()
    {
      var strategy = new CopyDirectoryStrategy();
      strategy.Source = this.source.ParsedValue;
      strategy.Destination = this.destination.ParsedValue;
      strategy.Replace = this.replace.ParsedValue;
      strategy.Rename = this.renameTo.ParsedValue;
      strategy.Recursive = this.recursive.ParsedValue;
      strategy.Copy();
      return 0;
    }
  }
}

