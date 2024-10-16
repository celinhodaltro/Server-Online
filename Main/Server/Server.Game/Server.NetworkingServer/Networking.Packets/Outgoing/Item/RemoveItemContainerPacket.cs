using Server.Entities.Common.Contracts.Items;
using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Outgoing.Item;

public class RemoveItemContainerPacket : OutgoingPacket
{
    private readonly byte containerId;
    private readonly IItem item;
    private readonly byte slotIndex;

    public RemoveItemContainerPacket(byte containerId, byte slotIndex, IItem item)
    {
        this.containerId = containerId;
        this.slotIndex = slotIndex;
        this.item = item;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.ContainerRemoveItem);

        message.AddByte(containerId);

        message.AddByte(slotIndex);
    }
}