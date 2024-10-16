using System;
using Networking.Handlers;
using Server.Common.Contracts.Network;

namespace Networking.Protocols;

public class LoginProtocol : Protocol
{
    private readonly Func<IConnection, IPacketHandler> _handlerFactory;

    public LoginProtocol(Func<IConnection, IPacketHandler> packetFactory)
    {
        _handlerFactory = packetFactory;
    }

    public override bool KeepConnectionOpen => false;

    public override void ProcessMessage(object sender, IConnectionEventArgs args)
    {
        var handler = _handlerFactory(args.Connection);
        handler.HandleMessage(args.Connection.InMessage, args.Connection);
    }

    public override string ToString() => "Login Protocol";
}