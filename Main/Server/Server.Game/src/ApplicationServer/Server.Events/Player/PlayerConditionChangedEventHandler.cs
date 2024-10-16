using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Parsers;
using Networking.Packets.Outgoing.Player;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerConditionChangedEventHandler
{
    private readonly IGameServer game;

    public PlayerConditionChangedEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICreature creature, ICondition c)
    {
        if (creature is not IPlayer player) return;
        if (!game.CreatureManager.GetPlayerConnection(creature.CreatureId, out var connection)) return;

        ushort icons = 0;
        foreach (var condition in player.Conditions) icons |= (ushort)ConditionIconParser.Parse(condition.Key);

        connection.OutgoingPackets.Enqueue(new ConditionIconPacket(icons));
        connection.Send();
    }
}