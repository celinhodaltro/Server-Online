using Game.Common.Contracts.Items;
using Game.Common.Location.Structs;

namespace Game.Items.Bases;

public class Item : BaseItem
{
    public Item(IItemType metadata, Location location) : base(metadata, location)
    {
    }
}