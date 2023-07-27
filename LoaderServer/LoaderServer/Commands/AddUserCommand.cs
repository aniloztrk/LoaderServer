using LoaderServer.Config.Models;
using LoaderServer.Core;
using LoaderServer.Interfaces;
using LoaderServer.Utils;

namespace LoaderServer.Commands;

public class AddUserCommand : ICommand
{
    public string Name => "adduser";

    public string Usage => "/adduser <pluginName> <discordId> <userName> <userIp>";
    
    public void Execute(string[] args)
    {
        if (args.Length != 4)
        {
            Logger.Warn("Komutu yanlış kullandın!", ConsoleColor.Red);
            Logger.Warn(Usage);
            return;
        }

        var pluginName = args[0];
        var discordId = args[1];
        var userName = args[2];
        var userIp = args[3];
        
        if (!new DirectoryInfo(Program.PluginsPath).GetFiles().Select(f => f.Name).Contains(pluginName + ".config.json"))
        {
            Logger.Print("AddUser","Plugin bulunamadı.", ConsoleColor.Red);
            return;
        }

        var pluginPath = Path.Combine(Program.PluginsPath, pluginName + ".config.json");

        if (!Json.TryDeserialize<Plugin>(out var plugin, pluginPath))
        {
            Logger.Print("AddUser","Plugin dosyası okunamadı.", ConsoleColor.Red);
            return;
        }

        if (plugin.Users.Any(u => u.DiscordId == discordId && u.Name == userName && u.IpAddress == userIp))
        {
            Logger.Print("AddUser","Bu bilgilerin aynısına sahip bir kullanıcı zaten var.", ConsoleColor.Red);
            return;
        }
        
        var user = new PluginUser(discordId, userName, userIp);
        
        plugin.Users.Add(user);

        Json.TrySerialize(plugin, pluginPath);
        Logger.Print("AddUser", "Kullanıcı başarıyla eklendi :)", ConsoleColor.Green);
    }
}