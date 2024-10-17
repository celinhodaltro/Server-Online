using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Outgoing.Login;

public class ReLoginWindowOutgoingPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.ReLoginWindow);
    }
}