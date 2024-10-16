using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Trade;

public class TradeClosePacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.TradeClose);
    }
}