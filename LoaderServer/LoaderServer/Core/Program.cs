using LoaderServer.Commands;
using LoaderServer.Config;
using LoaderServer.Config.Models;
using LoaderServer.Interfaces;
using LoaderServer.Listeners;
using LoaderServer.Utils;

namespace LoaderServer.Core;

public class Program
{
    public static readonly string ConfigPath = Path.Combine(Environment.CurrentDirectory, "Server/Config.json");
    
    public static readonly string MySqlConfigPath = Path.Combine(Environment.CurrentDirectory, "Server/MySqlConfig.json");

    public static readonly string PluginsPath = Path.Combine(Environment.CurrentDirectory, "Server/Plugins");

    public static Program? Instance { get; private set; }
    
    public ConfigRoot? Configuration { get; set; }
    
    public MySqlConfigRoot? MySqlConfiguration { get; set; }
    
    public List<IListener>? Listeners { get; set; }

    public List<ICommand>? Commands { get; set; }

    private static void Main()
    {
        Instance = new Program();
        Instance.MainAsync().GetAwaiter().GetResult();
    }

    private async Task MainAsync()
    {
        Configuration = new ConfigRoot();
        MySqlConfiguration = new MySqlConfigRoot();
        
        if (File.Exists(ConfigPath))
            Configuration.Load(true);
        else
            Configuration.SaveDefaults();
        
        if (File.Exists(MySqlConfigPath))
            MySqlConfiguration.Load(true);
        else
            MySqlConfiguration.SaveDefaults();

        if (!Directory.Exists(PluginsPath))
            Directory.CreateDirectory(PluginsPath);

        foreach (var pluginFileName in new DirectoryInfo(PluginsPath).GetFiles().Where(f => f.Name.EndsWith(".dll")).Select(f => f.Name.Replace(".dll", "")))
        {
            var pluginPath = Path.Combine(PluginsPath, pluginFileName + ".config.json");
            if (!File.Exists(pluginPath) || (File.Exists(pluginPath) && (await File.ReadAllTextAsync(pluginPath)).Length == 0))
            {
                if(!File.Exists(pluginPath))
                    File.Create(pluginPath).Close();
                
                Json.TrySerialize(new Plugin(pluginFileName), pluginPath);
            }
        }
        
        Commands = new List<ICommand>
        {
            new HelpCommand(),
            new ReloadConfigCommand(),
            new TestCommand(),
            new AddUserCommand(),
            new AesKeyGenerator()
        };

        Listeners = new List<IListener>
        {
            new CommandListener(),
            new ServerListener(),
            new WebRequestListener()
        };
        
        Listeners.ForEach(l => l.Create());
    }
}

