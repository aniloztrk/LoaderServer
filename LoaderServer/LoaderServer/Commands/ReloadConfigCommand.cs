using LoaderServer.Core;
using LoaderServer.Interfaces;

namespace LoaderServer.Commands;

public class ReloadConfigCommand : ICommand
{
    public string Name => "configreload";

    public string Usage => "/configreload";
    
    public void Execute(string[] args)
    {
        Program.Instance.Configuration.Load();
        Program.Instance.MySqlConfiguration.Load();
    }
}