using Server.Entities.Common.Contracts.Creatures;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Player;

public class PlayerWalkCancelPacket : OutgoingPacket
{
    private readonly IPlayer player;

    public PlayerWalkCancelPacket(IPlayer player)
    {
        this.player = player;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.PlayerWalkCancel);
        message.AddByte((byte)player.Direction);
    }
}