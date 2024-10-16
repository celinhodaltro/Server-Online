using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Networking.Packets.Outgoing.Player;
using Server.Contracts.Contracts;

namespace Server.Events.Player.Containers;

public class PlayerClosedContainerEventHandler
{
    private readonly IGameServer _game;

    public PlayerClosedContainerEventHandler(IGameServer game)
    {
        _game = game;
    }

    public void Execute(IPlayer player, byte containerId, IContainer container)
    {
        if (!_game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new CloseContainerPacket(containerId));
        connection.Send();
    }
}