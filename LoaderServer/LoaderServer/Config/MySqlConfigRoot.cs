using LoaderServer.Core;
using LoaderServer.Interfaces;
using LoaderServer.Utils;

namespace LoaderServer.Config;

[Serializable]
public class MySqlConfigRoot : IConfig
{
    public string Server { get; set; }
        
    public string Database { get; set; }
        
    public string UID { get; set; }
    
    public string Password { get; set; }
    
    public string Port { get; set; }
    
    public MySqlConfigRoot()
    {
        Server = "localhost";
        Database = "LoaderServer";
        UID = "root";
        Password = "";
        Port = "3306";
    }
    
    public void Load(bool sendMessage = false)
    {
        if (Json.TryDeserialize<MySqlConfigRoot>(out var config, Program.MySqlConfigPath))
        {
            Program.Instance.MySqlConfiguration = config;
            if(sendMessage) Logger.Print("MySqlConfiguration", "MySqlConfig dosyası başarıyla okundu.", ConsoleColor.Green);
        }
        else
            if(sendMessage) Logger.Print("MySqlConfiguration", "MySqlConfig dosyası okunamadı!", ConsoleColor.Red);
    }

    public void SaveDefaults()
    {
        var configDir = Path.Combine(Environment.CurrentDirectory, "Server");
        if (!Directory.Exists(configDir))
            Directory.CreateDirectory(configDir);
        
        File.Create(Program.MySqlConfigPath).Close();
        
        if (Json.TrySerialize<MySqlConfigRoot>(Program.Instance.MySqlConfiguration, Program.MySqlConfigPath))
            Logger.Print("MySqlConfiguration", "Varsayılan config dosyası yüklendi.");
    }

    public void Save()
    {
        Json.TrySerialize<MySqlConfigRoot>(Program.Instance.MySqlConfiguration, Program.MySqlConfigPath);
    }
}