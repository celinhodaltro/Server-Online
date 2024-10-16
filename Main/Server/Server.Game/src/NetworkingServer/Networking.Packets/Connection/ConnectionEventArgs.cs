using System;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Connection;

public class ConnectionEventArgs : EventArgs, IConnectionEventArgs
{
    public ConnectionEventArgs(Connection connection)
    {
        Connection = connection;
    }

    public IConnection Connection { get; }
}