using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Spells;
using Server.Entities.Common.Creatures;
using Networking.Packets.Outgoing;
using Networking.Packets.Outgoing.Effect;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerCannotUseSpellEventHandler
{
    private readonly IGameServer game;

    public PlayerCannotUseSpellEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICreature creature, ISpell spell, InvalidOperation error)
    {
        foreach (var spectator in game.Map.GetPlayersAtPositionZone(creature.Location))
        {
            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            connection.OutgoingPackets.Enqueue(new MagicEffectPacket(creature.Location, EffectT.Puff));
            connection.OutgoingPackets.Enqueue(new TextMessagePacket(TextMessageOutgoingParser.Parse(error),
                TextMessageOutgoingType.MESSAGE_STATUS_DEFAULT));
            connection.Send();
        }
    }
}