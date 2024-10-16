using Server.Common.Contracts.Network;

namespace Networking.Packets.Incoming.Player;

public class PlayerRequestOutFitPacket : IncomingPacket
{
    public PlayerRequestOutFitPacket(IReadOnlyNetworkMessage message)
    {
    }
}