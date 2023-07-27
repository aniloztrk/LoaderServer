namespace LoaderServer.Config.Models;

[Serializable]
public class PluginUser
{
    public string DiscordId { get; set; }
    
    public string Name { get; set; }
    
    public string IpAddress { get; set; }
    
    public string FirstAddTime { get; set; }
    
    public string LastChangeTime { get; set; }

    public PluginUser()
    {
        DiscordId = "123";
        Name = "mixy";
        IpAddress = "1.1.1.1";
        FirstAddTime = DateTime.Now.ToString("f");
        LastChangeTime = DateTime.Now.ToString("f");
    }

    public PluginUser(string discordId, string name, string ip)
    {
        DiscordId = discordId;
        Name = name;
        IpAddress = ip;
        FirstAddTime = DateTime.Now.ToString("f");
        LastChangeTime = DateTime.Now.ToString("f");
    }
}