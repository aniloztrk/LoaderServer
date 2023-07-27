using System.Diagnostics;
using LoaderServer.Interfaces;
using LoaderServer.Utils;

namespace LoaderServer.Commands;

public class AesKeyGenerator : ICommand
{
    public string Name => "aeskey";
    
    public string Usage => "/aeskey <length>";
    
    public void Execute(string[] args)
    {
        if (args.Length != 1)
        {
            Logger.Print("AesKey", Usage, ConsoleColor.Red);
            return;
        }

        if (!uint.TryParse(args[0], out var lenght))
        {
            Logger.Print("AesKey", "Geçersiz sayı.", ConsoleColor.Red);
            return;
        }
        
        var pCharacters = new Dictionary<int, string>
        {
            {0, "acbdefghjklmnoprstuvyzwxq"}, 
            {1, "ABCDEFGHJKLMNOPRSTUVYZWXQ"}, 
            {2, "0123456789"}, 
            {3, "!'^#+$£%&/{([)]=}?_|@"}
        };
    
        var random = new Random();
        var pArray = new char[lenght];

        var sw = new Stopwatch();
        sw.Start();
        
        for (uint i = 0; i < lenght; i++)
        {
            var _1 = random.Next(pCharacters.Count);
            var _2 = random.Next(pCharacters[_1].Length);
            pArray[i] = pCharacters[_1][_2];
        }
        
        var password = new string(pArray);
        Logger.Print("AesKey", password, ConsoleColor.Cyan);
        
        sw.Stop();
        Logger.Print("AesKey", "Oluşturma süresi: " + sw.Elapsed, ConsoleColor.Magenta);

    }
}