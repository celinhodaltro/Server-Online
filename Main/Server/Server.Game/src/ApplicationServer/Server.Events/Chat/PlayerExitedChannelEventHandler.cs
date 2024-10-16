using Server.Entities.Common.Contracts.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Networking.Packets.Outgoing.Chat;
using Server.Common.Contracts;

namespace Server.Events.Chat;

public class PlayerExitedChannelEventHandler
{
    private readonly IGameServer game;

    public PlayerExitedChannelEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player, IChatChannel channel)
    {
        if (channel is null) return;
        if (player is null) return;
        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new PlayerCloseChannelPacket(channel.Id));
        connection.Send();
    }
}