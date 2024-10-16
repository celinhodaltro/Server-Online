using Server.Entities.Common.Location.Structs;
using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Incoming.Trade;

public class TradeRequestPacket : IncomingPacket
{
    public TradeRequestPacket(IReadOnlyNetworkMessage message)
    {
        Location = message.GetLocation();
        ClientId = message.GetUInt16();
        StackPosition = message.GetByte();
        PlayerId = message.GetUInt32();
    }

    public uint PlayerId { get; }
    public byte StackPosition { get; }
    public ushort ClientId { get; }
    public Location Location { get; }
}