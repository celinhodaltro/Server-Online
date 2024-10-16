﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Networking.Packets.Connection;
using Networking.Protocols;
using Server.Common.Contracts.Network;
using Serilog;

namespace Networking.Listeners;

public abstract class Listener : TcpListener, IListener
{
    private readonly ILogger _logger;
    private readonly int _port;
    private readonly IProtocol _protocol;

    protected Listener(int port, IProtocol protocol, ILogger logger) : base(IPAddress.Any, port)
    {
        _port = port;
        _protocol = protocol;
        _logger = logger;
    }

    public void BeginListening(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            try
            {
                Start();
            }
            catch (SocketException ex)
            {
                _logger.Error(ex, "Could not start {Protocol} on port {Port}", _protocol, _port);
                Environment.Exit(1);
                return;
            }

            _logger.Information("{Protocol} is online", _protocol);

            while (!cancellationToken.IsCancellationRequested)
            {
                var connection = await CreateConnection(cancellationToken);

                _protocol.OnAccept(connection);
            }
        }, cancellationToken);
    }

    public void EndListening() => Stop();

    private async Task<IConnection> CreateConnection(CancellationToken cancellationToken)
    {
        var socket = await AcceptSocketAsync(cancellationToken).ConfigureAwait(false);

        var connection = new Connection(socket, _logger);

        connection.OnCloseEvent += OnConnectionClose;
        connection.OnProcessEvent += _protocol.ProcessMessage;
        connection.OnPostProcessEvent += _protocol.PostProcessMessage;
        return connection;
    }

    private void OnConnectionClose(object sender, IConnectionEventArgs args)
    {
        // De-subscribe to this event first.
        args.Connection.OnCloseEvent -= OnConnectionClose;
        args.Connection.OnProcessEvent -= _protocol.ProcessMessage;
        args.Connection.OnPostProcessEvent -= _protocol.PostProcessMessage;
    }
}