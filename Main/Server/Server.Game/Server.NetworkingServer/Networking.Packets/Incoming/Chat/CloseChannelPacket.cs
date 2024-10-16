using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Incoming.Chat;

public class CloseChannelPacket : IncomingPacket
{
    public CloseChannelPacket(IReadOnlyNetworkMessage message)
    {
        ChannelId = message.GetUInt16();
    }

    public ushort ChannelId { get; set; }
}