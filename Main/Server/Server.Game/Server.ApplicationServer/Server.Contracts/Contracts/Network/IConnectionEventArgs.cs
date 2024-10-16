namespace Server.Common.Contracts.Network;

public interface IConnectionEventArgs
{
    IConnection Connection { get; }
}