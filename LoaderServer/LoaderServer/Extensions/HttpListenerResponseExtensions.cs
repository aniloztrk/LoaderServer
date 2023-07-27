using System.Net;
using System.Text;

namespace LoaderServer.Extensions;

public static class HttpListenerResponseExtensions
{
    public static void Write(this HttpListenerResponse response, string data)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);
        response.OutputStream.Write(dataBytes, 0, dataBytes.Length);
    }
    
    public static void Write(this HttpListenerResponse response, byte[] data)
    {
        response.OutputStream.Write(data, 0, data.Length);
    }
}