using System;

namespace MinionCopy.ConsoleApp
{
  class App
  {
    static int Main(string[] args)
    {
      try
      {
        var app = new ConsoleToolAdapter().Command;
        return app.Execute(args);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }
  }
}
