using Server.Entities.Common.Combat;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Networking.Packets.Outgoing.Effect;
using Server.Common.Contracts;

namespace Server.Events.Combat;

public class CreatureBlockedAttackEventHandler
{
    private readonly IGameServer game;

    public CreatureBlockedAttackEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICreature creature, BlockType blockType)
    {
        foreach (var spectator in game.Map.GetPlayersAtPositionZone(creature.Location))
        {
            var effect = blockType == BlockType.Armor ? EffectT.SparkYellow : EffectT.Puff;

            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            connection.OutgoingPackets.Enqueue(new MagicEffectPacket(creature.Location, effect));
            connection.Send();
        }
    }
}