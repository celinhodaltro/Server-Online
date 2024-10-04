using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Player;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;

namespace Server.Events.Player;

public abstract class PlayerLevelChangeEventHandler
{
    private readonly IGameServer game;

    protected PlayerLevelChangeEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player, SkillType skillType, int fromLevel, int toLevel)
    {
        if (Guard.IsNull(player)) return;

        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        SendLevelChangeMessage(skillType, connection, fromLevel, toLevel);

        connection.OutgoingPackets.Enqueue(new PlayerStatusPacket(player));

        connection.Send();
    }

    protected abstract void SendLevelChangeMessage(SkillType skillType, IConnection connection, int fromLevel,
        int toLevel);
}