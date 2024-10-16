namespace Server.Contracts.Contracts;

public interface IStartup
{
    void Run();
}

public interface IRunBeforeLoader
{
    void Run();
}