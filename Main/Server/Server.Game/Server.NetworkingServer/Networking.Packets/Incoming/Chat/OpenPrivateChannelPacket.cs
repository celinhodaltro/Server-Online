using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Incoming.Chat;

public class OpenPrivateChannelPacket : IncomingPacket
{
    public OpenPrivateChannelPacket(IReadOnlyNetworkMessage message)
    {
        Receiver = message.GetString();
    }

    public string Receiver { get; }
}