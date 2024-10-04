using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Texts;
using Networking.Packets.Outgoing;
using Networking.Packets.Outgoing.Effect;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerExhaustedEventHandler
{
    private readonly IGameServer game;

    public PlayerExhaustedEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player)
    {
        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new MagicEffectPacket(player.Location, EffectT.Puff));
        connection.OutgoingPackets.Enqueue(new TextMessagePacket(TextConstants.YOU_ARE_EXHAUSTED,
            TextMessageOutgoingType.Small));

        connection.Send();
    }
}