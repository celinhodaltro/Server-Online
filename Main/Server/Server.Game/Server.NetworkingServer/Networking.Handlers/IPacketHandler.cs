using Server.Common.Contracts.Network;

namespace Networking.Handlers;

public interface IPacketHandler
{
    void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection);
}