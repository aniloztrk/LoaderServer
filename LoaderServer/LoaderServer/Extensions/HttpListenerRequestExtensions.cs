using System.Net;

namespace LoaderServer.Extensions;

public static class HttpListenerRequestExtensions
{
    public static string GetPost(this HttpListenerRequest request)
    {
        var reader = new StreamReader(request.InputStream, request.ContentEncoding);
        return reader.ReadToEnd();
    }
}
