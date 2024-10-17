using Server.Entities.Common.Location.Structs;
using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Incoming;

public class UseItemPacket : IncomingPacket
{
    public UseItemPacket(IReadOnlyNetworkMessage message)
    {
        Location = message.GetLocation();
        ClientId = message.GetUInt16();
        StackPosition = message.GetByte();
        Index = message.GetByte();
    }

    public Location Location { get; }
    public ushort ClientId { get; }
    public byte StackPosition { get; set; }
    public byte Index { get; set; }
}