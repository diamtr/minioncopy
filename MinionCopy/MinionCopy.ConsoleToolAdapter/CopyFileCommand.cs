using McMaster.Extensions.CommandLineUtils;

namespace MinionCopy
{
  public class CopyFileCommand : CommandLineApplication
  {
    CommandArgument<string> source;
    CommandArgument<string> destination;
    CommandOption<bool> replace;
    CommandOption<string> renameTo;

    public CopyFileCommand()
    {
      this.Name = "file";
      this.Description = "Copy single file";
      this.source = this.Argument<string>("source", "Source file path to copy.").IsRequired();
      this.destination = this.Argument<string>("destination", "Destination path to copy").IsRequired();
      this.replace = this.Option<bool>("--replace", "Replace file if it already exists in destination.", CommandOptionType.NoValue);
      this.renameTo = this.Option<string>("--rename", "Rename file in destination to specified name while copying.", CommandOptionType.SingleValue);
      this.OnExecute(this.Execute);
    }

    public int Execute()
    {
      var strategy = new CopyFileStrategy();
      strategy.Source = this.source.ParsedValue;
      strategy.Destination = this.destination.ParsedValue;
      strategy.Replace = this.replace.ParsedValue;
      strategy.Rename = this.renameTo.ParsedValue;
      strategy.Copy();
      return 0;
    }
  }
}
