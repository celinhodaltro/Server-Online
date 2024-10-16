using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Networking.Packets.Outgoing.Chat;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerSentMessageEventHandler
{
    private readonly IGameServer game;

    public PlayerSentMessageEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ISociableCreature from, ISociableCreature to, SpeechType speechType, string message)
    {
        if (string.IsNullOrWhiteSpace(message) || to is null || from is null) return;

        if (!game.CreatureManager.GetPlayerConnection(to.CreatureId, out var receiverConnection)) return;

        receiverConnection.OutgoingPackets.Enqueue(new PlayerSendPrivateMessagePacket(from, speechType, message));
        receiverConnection.Send();
    }
}