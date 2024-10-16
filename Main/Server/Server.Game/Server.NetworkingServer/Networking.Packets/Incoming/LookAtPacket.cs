using Server.Entities.Common.Location.Structs;
using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Incoming;

public class LookAtPacket : IncomingPacket
{
    public LookAtPacket(IReadOnlyNetworkMessage message)
    {
        Location = message.GetLocation();
        message.SkipBytes(2); //sprite id
        StackPosition = message.GetByte();
    }

    public Location Location { get; set; }
    public byte StackPosition { get; set; }
}