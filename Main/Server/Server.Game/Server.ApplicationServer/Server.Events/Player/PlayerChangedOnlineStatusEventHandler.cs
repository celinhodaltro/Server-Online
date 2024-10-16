using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Chat;
using Server.Contracts.Contracts;

namespace Server.Events.Player;

public class PlayerChangedOnlineStatusEventHandler : IEventHandler
{
    private readonly IGameServer game;

    public PlayerChangedOnlineStatusEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player, bool online)
    {
        if (player.IsNull()) return;

        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out _)) return;

        foreach (var loggedPlayer in game.CreatureManager.GetAllLoggedPlayers())
        {
            if (!loggedPlayer.Vip.HasInVipList(player.Id)) continue;
            if (!game.CreatureManager.GetPlayerConnection(loggedPlayer.CreatureId, out var friendConnection))
                continue;

            friendConnection.OutgoingPackets.Enqueue(new PlayerUpdateVipStatusPacket(player.Id, online));
            friendConnection.Send();
        }
    }
}