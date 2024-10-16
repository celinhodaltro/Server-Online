namespace Server.Common.Contracts;

public interface IStartup
{
    void Run();
}

public interface IRunBeforeLoader
{
    void Run();
}