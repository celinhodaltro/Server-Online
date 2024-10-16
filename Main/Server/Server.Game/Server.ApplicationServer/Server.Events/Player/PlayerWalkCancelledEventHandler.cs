using Server.Entities.Common.Contracts.Creatures;
using Networking.Packets.Outgoing.Player;
using Server.Contracts.Contracts;

namespace Server.Events.Player;

public class PlayerWalkCancelledEventHandler
{
    private readonly IGameServer game;

    public PlayerWalkCancelledEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICreature creature)
    {
        if (creature is not IPlayer player) return;

        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new PlayerWalkCancelPacket(player));
        connection.Send();
    }
}