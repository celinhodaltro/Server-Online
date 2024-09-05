using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing;

public class PingPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.Ping);
    }
}