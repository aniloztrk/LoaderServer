using LoaderServer.Core;
using LoaderServer.Interfaces;
using LoaderServer.Utils;

namespace LoaderServer.Commands;

public class HelpCommand : ICommand
{
    public string Name => "help";

    public string Usage => "/help";
    
    public void Execute(string[] args)
    {
        Logger.Print("Help", Program.Instance.Commands.Count + " tane komut listeleniyor...", ConsoleColor.Green);

        foreach (var command in Program.Instance.Commands)
        {
            Logger.Print("Help", $"/{command.Name} - Kullanım: {command.Usage}", ConsoleColor.Cyan);
        }
    }
}