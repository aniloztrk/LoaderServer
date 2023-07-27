using LoaderServer.Config.Models;
using LoaderServer.Core;
using LoaderServer.Interfaces;
using LoaderServer.Utils;

namespace LoaderServer.Config;

[Serializable]
public class ConfigRoot : IConfig
{
    public string Ip { get; set; }
    
    public string ResponsePort { get; set; }
    
    public string RequestPort { get; set; }
    
    public string LoaderVersion { get; set; }
    
    public Webhook Webhook { get; set; }

    public ConfigRoot()
    {
        Ip = "localhost";
        ResponsePort = "3090";
        RequestPort = "3091";
        LoaderVersion = "1.0.0.3";
        Webhook = new Webhook();
    }

    public void Load(bool sendMessage = false)
    {
        if (Json.TryDeserialize<ConfigRoot>(out var config, Program.ConfigPath))
        {
            Program.Instance.Configuration = config;
            if(sendMessage) Logger.Print("Configuration", "Config dosyası başarıyla okundu.", ConsoleColor.Green);
        }
        else
            if(sendMessage) Logger.Print("Configuration", "Config dosyası okunamadı!", ConsoleColor.Red);
    }

    public void SaveDefaults()
    {
        var configDir = Path.Combine(Environment.CurrentDirectory, "Server");
        if (!Directory.Exists(configDir))
            Directory.CreateDirectory(configDir);
        
        File.Create(Program.ConfigPath).Close();
        
        if (Json.TrySerialize<ConfigRoot>(Program.Instance.Configuration, Program.ConfigPath))
            Logger.Print("Configuration", "Varsayılan config dosyası yüklendi.", ConsoleColor.Gray);
        
    }

    public void Save()
    {
        Json.TrySerialize<ConfigRoot>(Program.Instance.Configuration, Program.ConfigPath);
    }
}