using Server.Contracts.Contracts.Network;

namespace Networking.Handlers;

public abstract class PacketHandler : IPacketHandler
{
    public abstract void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection);
}