using System.Text.Json;

namespace LoaderServer.Utils;

public class Json
{
    public static bool TrySerialize<T>(T obj, string path)
    {
        try
        {
            var jsonObj = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, jsonObj);
            return true;
        }
        catch
        {
            Logger.Print("Json", $"{path} \n dosyasına yazılamadı!", ConsoleColor.Red);
            return false;
        }
    }

    public static bool TryDeserialize<T>(out T? obj, string path)
    {
        try
        {
            obj = JsonSerializer.Deserialize<T>(File.ReadAllText(path));
            return true;
        } 
        catch
        {
            Logger.Print("Json", $"{path} \n okunamadı!", ConsoleColor.Red);
            obj = default;
            return false;
        }
    }

    public static bool TryDeserializeFromString<T>(out T? obj, string data)
    {
        try
        {
            obj = JsonSerializer.Deserialize<T>(data);
            return true;
        } 
        catch
        {
            Logger.Print("Json", $"{data} \n okunamadı!", ConsoleColor.Red);
            obj = default;
            return false;
        }
    }
}