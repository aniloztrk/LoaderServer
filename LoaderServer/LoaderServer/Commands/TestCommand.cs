using LoaderServer.Interfaces;
using LoaderServer.Utils;

namespace LoaderServer.Commands;

public class TestCommand : ICommand
{
    public string Name => "test";
    
    public string Usage => "";
    
    public void Execute(string[] args)
    {
    }
}