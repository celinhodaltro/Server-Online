using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Player;
using Server.Contracts.Contracts;

namespace Server.Events.Combat;

public class CreatureStoppedAttackEventHandler
{
    private readonly IGameServer game;

    public CreatureStoppedAttackEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICombatActor actor)
    {
        if (Guard.IsNull(actor)) return;

        if (!game.CreatureManager.GetPlayerConnection(actor.CreatureId, out var connection)) return;
        connection.OutgoingPackets.Enqueue(new CancelTargetPacket());
        connection.Send();
    }
}