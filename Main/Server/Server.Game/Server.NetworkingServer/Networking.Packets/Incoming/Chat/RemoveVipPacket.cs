using Server.Common.Contracts.Network;

namespace Networking.Packets.Incoming.Chat;

public class RemoveVipPacket : IncomingPacket
{
    public RemoveVipPacket(IReadOnlyNetworkMessage message)
    {
        PlayerId = message.GetUInt32();
    }

    public uint PlayerId { get; set; }
}