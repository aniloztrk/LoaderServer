using System.Text.Json;
using LoaderServer.Utils;

namespace LoaderServer.Requests;

public class IpChangeRequest 
{
    public string UserID { get; set; }
    
    public string PluginLicense { get; set; }
    
    public string NewIp { get; set; }

    public IpChangeRequest(string user, string license, string ip)
    {
        UserID = user;
        PluginLicense = license;
        NewIp = ip;
    }

    public IpChangeRequest() { }

    public static IpChangeRequest? GetFromJson(string json)
    {
        if (Json.TryDeserializeFromString<IpChangeRequest>(out var request, json))
            return request;
        
        return null;
    }
}