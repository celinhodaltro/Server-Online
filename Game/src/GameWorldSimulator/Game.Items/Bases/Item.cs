using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Location.Structs;

namespace Game.Items.Bases;

public class Item : BaseItem
{
    public Item(IItemType metadata, Location location) : base(metadata, location)
    {
    }
}