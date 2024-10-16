namespace Server.Contracts.Contracts.Network;

public interface IConnectionEventArgs
{
    IConnection Connection { get; }
}