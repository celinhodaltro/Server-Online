using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Outgoing;

public abstract class OutgoingPacket : IOutgoingPacket
{
    public virtual bool Disconnect { get; protected set; } = false;
    public abstract void WriteToMessage(INetworkMessage message);
}