using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Networking.Packets.Outgoing;
using Server.Contracts.Contracts;

namespace Server.Events.Player;

public class NotificationSentEventHandler
{
    private readonly IGameServer game;

    public NotificationSentEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IThing thing, string message, NotificationType notificationType)
    {
        if (thing is not IPlayer player) return;
        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        var type = notificationType switch
        {
            NotificationType.Description => TextMessageOutgoingType.Description,
            NotificationType.Information => TextMessageOutgoingType.MESSAGE_EVENT_LEVEL_CHANGE,
            _ => TextMessageOutgoingType.Description
        };

        connection.OutgoingPackets.Enqueue(new TextMessagePacket(message,
            type));
        connection.Send();
    }
}