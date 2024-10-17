using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Outgoing.Trade;

public class TradeClosePacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.TradeClose);
    }
}