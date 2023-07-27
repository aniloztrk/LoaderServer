namespace LoaderServer.Config.Models;

[Serializable]
public class Webhook
{
    public string Url { get; set; }
        
    public string Username { get; set; }
        
    public string AvatarUrl { get; set; }

    public Webhook()
    {
        Url = "";
        Username = "Mixy Plugin Loader";
        AvatarUrl = "";
    }
}