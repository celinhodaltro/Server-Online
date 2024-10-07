using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures.Players;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Player;

public class PlayerInventoryItemPacket : OutgoingPacket
{
    private readonly IInventory inventory;
    private readonly Slot slot;

    public PlayerInventoryItemPacket(IInventory inventory, Slot slot)
    {
        this.inventory = inventory;
        this.slot = slot;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        if (inventory[slot] == null)
        {
            message.AddByte((byte)GameOutgoingPacketType.InventoryEmpty);
            message.AddByte((byte)slot);
        }
        else
        {
            message.AddByte((byte)GameOutgoingPacketType.InventoryItem);
            message.AddByte((byte)slot);
            message.AddItem(inventory[slot]);
        }
    }
}