using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Networking.Packets.Outgoing;
using Server.Common.Contracts;

namespace Server.Events;

public class InvalidOperationEventHandler
{
    private readonly IGameServer game;

    public InvalidOperationEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IThing thing, InvalidOperation error)
    {
        if (thing is not IPlayer player) return;
        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new TextMessagePacket(TextMessageOutgoingParser.Parse(error),
            TextMessageOutgoingType.Small));
        connection.Send();
    }
}