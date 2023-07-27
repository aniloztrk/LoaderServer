using LoaderServer.Config.Models;
using LoaderServer.Core;

namespace LoaderServer.Utils;

public class PluginUtils
{
    public static void ChangeIp(string license, string userId, string newIp)
    {
        foreach (var pluginPath in new DirectoryInfo(Program.PluginsPath).GetFiles().Where(f => f.Name.EndsWith(".config.json")).Select(f => Path.Combine(Program.PluginsPath, f.Name)))
        {
            if (Json.TryDeserialize<Plugin>(out var plugin, pluginPath))
            {
                if (license != "all" && license != plugin.License) continue;
                
                var user = plugin.Users.FirstOrDefault(u => u.DiscordId == userId);
                if(user == null) continue;

                user.IpAddress = newIp;
                user.LastChangeTime = DateTime.Now.ToString("f");

                Json.TrySerialize(plugin, pluginPath);
                Logger.Print("ChangeIp", "Ip adresi değiştirildi.", ConsoleColor.Green);
                Logger.Print("ChangeIp", $"Plugin: {plugin.Name} - User: {user.Name}", ConsoleColor.Cyan);
            }
            else
            {
                Logger.Print("ChangeIp", $"{pluginPath} Okunamadı!!!", ConsoleColor.Red);
            }
        }
    }

    public static Plugin? GetPluginFromLicense(string license)
    {
        foreach (var pluginPath in new DirectoryInfo(Program.PluginsPath).GetFiles().Where(f => f.Name.EndsWith(".config.json")).Select(f => Path.Combine(Program.PluginsPath, f.Name)))
        {
            if (!Json.TryDeserialize<Plugin>(out var plugin, pluginPath)) continue;
            if (plugin.License == license)
                return plugin;
        }

        return null;
    }

    public static bool TryGetIpListFromLicense(out string[] ipArray, string license)
    {
        try
        {
            var plugin = GetPluginFromLicense(license);

            var users = plugin.Users;
            if (users.Count != 0)
            {
                ipArray = users.Select(u => u.IpAddress).ToArray();
                return true;
            }
            
            ipArray = null;
            return false;
        }
        catch
        {
            ipArray = null;
            return false;
        }
    }

    public static bool TryGetPluginAssemblyFromLicense(out byte[]? assembly, string license)
    {
        try
        {
            var plugin = GetPluginFromLicense(license);
            var dllPath = Path.Combine(Program.PluginsPath, plugin.Name + ".dll");
            assembly = File.ReadAllBytes(dllPath);
            return true;
        }
        catch
        {
            assembly = null;
            return false;
        }
    }
}