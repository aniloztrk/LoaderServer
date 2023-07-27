using LoaderServer.Enums;

namespace LoaderServer.Models;

public class RequestPost
{
    public EPostType PostType { get; set; }
    
    public object PostedObject { get; set; }

    public RequestPost() { }
}