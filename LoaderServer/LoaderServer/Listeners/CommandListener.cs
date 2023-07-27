using LoaderServer.Core;
using LoaderServer.Interfaces;
using LoaderServer.Utils;

namespace LoaderServer.Listeners;

public class CommandListener : IListener
{
    public void Create()
    {
        new Thread(Start).Start();
    }
    
    public void Start()
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (!input.StartsWith("/"))
            {
                Logger.Warn("Geçersiz komut! '/help' yazarak tüm komutları görebilirsin.", ConsoleColor.Red);
                continue;
            }

            var allArgs = input.Split(' ');

            var commandString = allArgs[0];

            var command = Program.Instance.Commands.FirstOrDefault(c => c.Name == commandString.Replace("/", ""));
            
            if (command == null)
            {
                Logger.Warn("Geçersiz komut! '/help' yazarak tüm komutları görebilirsin.", ConsoleColor.Red);
                continue;
            }

            var commandArgs = new string[allArgs.Length - 1];

            for (int i = 0; i < allArgs.Length - 1; i++)
            {
                commandArgs[i] = allArgs[i + 1];
            }
            
            command.Execute(commandArgs);
        }
    }
}