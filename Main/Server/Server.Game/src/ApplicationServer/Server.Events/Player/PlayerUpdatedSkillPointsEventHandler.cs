using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Player;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerUpdatedSkillPointsEventHandler
{
    private readonly IGameServer game;

    public PlayerUpdatedSkillPointsEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player, SkillType skill)
    {
        if (Guard.AnyNull(player)) return;
        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new PlayerSkillsPacket(player));
        connection.Send();
    }

    public void Execute(IPlayer player, SkillType skill, sbyte increased)
    {
        if (game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection))
        {
            connection.OutgoingPackets.Enqueue(new PlayerSkillsPacket(player));
            connection.Send();
        }
    }
}