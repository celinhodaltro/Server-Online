using Data.InMemory;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Window;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerReadTextEventHandler : IEventHandler
{
    private readonly IGameServer game;

    public PlayerReadTextEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player, IReadable readable, string text)
    {
        if (Guard.AnyNull(player, readable)) return;

        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        var id = ItemTextWindowStore.Add(player, readable);

        connection.OutgoingPackets.Enqueue(new TextWindowPacket(id, readable));

        connection.Send();
    }
}