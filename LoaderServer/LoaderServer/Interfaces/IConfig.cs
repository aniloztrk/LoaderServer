namespace LoaderServer.Interfaces;

public interface IConfig
{
    void Load(bool sendMessage);
    void SaveDefaults();
    void Save();
}