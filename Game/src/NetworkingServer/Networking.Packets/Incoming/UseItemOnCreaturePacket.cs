using Game.Common.Location.Structs;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Incoming;

public class UseItemOnCreaturePacket : IncomingPacket
{
    public UseItemOnCreaturePacket(IReadOnlyNetworkMessage message)
    {
        FromLocation = message.GetLocation();
        ClientId = message.GetUInt16();
        FromStackPosition = message.GetByte();
        CreatureId = message.GetUInt32();
    }

    public Location FromLocation { get; }
    public ushort ClientId { get; }
    public byte FromStackPosition { get; set; }
    public uint CreatureId { get; }
}