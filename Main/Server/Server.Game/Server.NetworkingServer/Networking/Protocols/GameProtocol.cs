﻿using System;
using Networking.Handlers;
using Networking.Packets.Security;
using Server.Common.Contracts.Network;

namespace Networking.Protocols;

public class GameProtocol : Protocol
{
    private readonly Func<IConnection, IPacketHandler> _handlerFactory;

    public GameProtocol(Func<IConnection, IPacketHandler> handlerFactory)
    {
        _handlerFactory = handlerFactory;
    }

    public override bool KeepConnectionOpen => true;

    public override string ToString()
    {
        return "Game Protocol";
    }

    public override void OnAccept(IConnection connection)
    {
        HandlerFirstConnection(connection);
        base.OnAccept(connection);
    }

    private void HandlerFirstConnection(IConnection connection)
    {
        connection.SendFirstConnection();
    }

    public override void ProcessMessage(object sender, IConnectionEventArgs args)
    {
        var connection = args.Connection;

        HandleMessage(args, connection);
    }

    private void HandleMessage(IConnectionEventArgs args, IConnection connection)
    {
        if (connection.IsAuthenticated && !connection.Disconnected)
            Xtea.Decrypt(connection.InMessage, 6, connection.XteaKey);

        if (_handlerFactory(args.Connection) is not { } handler) return;

        handler?.HandleMessage(args.Connection.InMessage, args.Connection);
    }
}