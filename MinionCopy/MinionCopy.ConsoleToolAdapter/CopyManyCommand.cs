using McMaster.Extensions.CommandLineUtils;

namespace MinionCopy
{
  public class CopyManyCommand : CommandLineApplication
  {
    CommandOption<string> list;

    public CopyManyCommand()
    {
      this.Name = "many";
      this.Description = "Copy many files/directories from list.";
      this.list = this.Option<string>("--list", "File name with copy list.", CommandOptionType.SingleValue).IsRequired();
      this.OnExecute(this.Execute);
    }

    public int Execute()
    {
      var strategy = new CopyFromListStrategy();
      strategy.Source = this.list.ParsedValue;
      strategy.Copy();
      return 0;
    }
  }
}
