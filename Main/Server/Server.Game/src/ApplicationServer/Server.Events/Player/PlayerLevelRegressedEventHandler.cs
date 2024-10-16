using Server.Entities.Common.Creatures;
using Server.Entities.Common.Parsers;
using Networking.Packets.Outgoing;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;

namespace Server.Events.Player;

public class PlayerLevelRegressedEventHandler : PlayerLevelChangeEventHandler
{
    public PlayerLevelRegressedEventHandler(IGameServer game) : base(game)
    {
    }

    protected override void SendLevelChangeMessage(SkillType skillType, IConnection connection, int fromLevel,
        int toLevel)
    {
        connection.OutgoingPackets.Enqueue(new TextMessagePacket(
            MessageParser.GetSkillRegressedMessage(skillType, fromLevel, toLevel),
            TextMessageOutgoingType.MESSAGE_EVENT_LEVEL_CHANGE));
    }
}