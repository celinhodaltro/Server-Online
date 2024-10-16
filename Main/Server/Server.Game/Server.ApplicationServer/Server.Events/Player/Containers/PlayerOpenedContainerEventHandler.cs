﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Networking.Packets.Outgoing.Player;
using Server.Common.Contracts;

namespace Server.Events.Player.Containers;

public class PlayerOpenedContainerEventHandler
{
    private readonly IGameServer game;

    public PlayerOpenedContainerEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player, byte containerId, IContainer container)
    {
        SendContainerPacket(player, containerId, container);
    }

    private void SendContainerPacket(IPlayer player, byte containerId, IContainer container)
    {
        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new OpenContainerPacket(container, containerId));
        connection.Send();
    }
}