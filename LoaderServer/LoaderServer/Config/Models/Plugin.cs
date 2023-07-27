namespace LoaderServer.Config.Models;

[Serializable]
public class Plugin
{
    public string Name { get; set; }
    
    public string License { get; set; }
    
    public List<PluginUser> Users { get; set; }

    public Plugin(string name)
    {
        Name = name;
        License = name.Replace("Mixy", "mixy-").Replace(".dll", "").Replace('I', 'i').ToLower();
        Users = new List<PluginUser>();
    }
    
    public Plugin() {}
}