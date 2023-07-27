using System.Net;
using System.Text.Json;
using LoaderServer.Core;
using LoaderServer.Enums;
using LoaderServer.Extensions;
using LoaderServer.Interfaces;
using LoaderServer.Models;
using LoaderServer.Requests;
using LoaderServer.Utils;

namespace LoaderServer.Listeners;

public class WebRequestListener : IListener
{
    public delegate void HttpPostRequestHandler(EPostType postType, ref object obj);

    public event HttpPostRequestHandler OnHttpPostRequested;
    
    public void Create()
    {
        OnHttpPostRequested += HttpPostRequest;
        
        new Thread(Start).Start();
    }

    private void HttpPostRequest(EPostType postType, ref object obj)
    {
        switch (postType)
        {
            case EPostType.IpChange:
                var changeRequest = JsonSerializer.Deserialize<IpChangeRequest>(obj.ToString());
                PluginUtils.ChangeIp(changeRequest.PluginLicense, changeRequest.UserID, changeRequest.NewIp);
                break;
        }
    }

    public void Start()
    {
        var httpListener = new HttpListener();
        httpListener.Prefixes.Add($"http://{Program.Instance.Configuration.Ip}:{Program.Instance.Configuration.RequestPort}/");
        httpListener.Start();

        while (true)
        {
            var request = httpListener.GetContext().Request;
            
            if (Json.TryDeserializeFromString<RequestPost>(out var post, request.GetPost()))
            {
                var obj = post.PostedObject;
                OnHttpPostRequested?.Invoke(post.PostType, ref obj);
            }
        }
    }
}