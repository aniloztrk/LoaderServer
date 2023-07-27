namespace LoaderServer.Interfaces;

public interface ICommand
{
    string Name { get; }
    string Usage { get; }
    void Execute(string[] args);
}