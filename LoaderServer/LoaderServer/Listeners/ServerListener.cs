using System.Net;
using System.Text.Json;
using LoaderServer.Core;
using LoaderServer.Extensions;
using LoaderServer.Interfaces;
using LoaderServer.Utils;

namespace LoaderServer.Listeners;

public class ServerListener : IListener
{
    public void Create()
    {
        new Thread(Start).Start();
    }

    public void Start()
    {
        var httpListener = new HttpListener();
        httpListener.Prefixes.Add($"http://{Program.Instance.Configuration.Ip}:{Program.Instance.Configuration.ResponsePort}/");
        httpListener.Start();

        while (true)
        {
            var context = httpListener.GetContext();
            
            var response = context.Response;
            response.AddHeader("Content-Type", "text/html; charset=utf-8");
            
            var request = context.Request;
            
            if(request.RawUrl == "/favicon.ico") continue;
            
            var license = request.RawUrl.Replace("/", "");

            var connectedIp = Dns.GetHostEntry(request.RemoteEndPoint.Address).HostName;
            
            if (PluginUtils.TryGetPluginAssemblyFromLicense(out var assembly, license))
            {
                response.Write(AesEncryption.EncryptBytes(assembly));
                Logger.Print("GetAssembly", $"Connected Address: {connectedIp} - License : {license}", ConsoleColor.Yellow, ConsoleColor.Cyan);
            }
            else if (request.RawUrl.EndsWith("/Ip") && PluginUtils.TryGetIpListFromLicense(out var ipArray, license.Replace("Ip", "")))
            {
                response.Write(AesEncryption.EncryptString(string.Join("<br />", ipArray)));
                Logger.Print("GetIpList", $"Connected Address: {connectedIp} - License : {license.Replace("Ip", "")}", ConsoleColor.Yellow, ConsoleColor.Cyan);
            }
            else if (request.RawUrl.Equals("/version"))
            {
                Program.Instance.Configuration.Load();
                response.Write(Program.Instance.Configuration.LoaderVersion);
                Logger.Print("GetVersion", $"Connected Address: {connectedIp} - License : {license.Replace("Ip", "")}", ConsoleColor.Yellow, ConsoleColor.Cyan);
            }
            else if (request.RawUrl.Equals("/webhook"))
            {
                Program.Instance.Configuration.Load();
                response.Write(AesEncryption.EncryptString(JsonSerializer.Serialize(Program.Instance.Configuration.Webhook)));
                Logger.Print("GetWebhook", $"Connected Address: {connectedIp} - License : {license.Replace("Ip", "")}", ConsoleColor.Yellow, ConsoleColor.Cyan);
            }
            else
            {
                response.Write("Sayfa bulunamadı.");
                Logger.Print("PageNotFound", $"Connected Address: {connectedIp}", ConsoleColor.Yellow, ConsoleColor.Red);
            }
            
            response.Close();
        }
    }
}