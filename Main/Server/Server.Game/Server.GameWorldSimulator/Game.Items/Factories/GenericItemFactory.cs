using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Location.Structs;
using Game.Items.Bases;

namespace Game.Items.Factories;

public class GenericItemFactory : IFactory
{
    public event CreateItem OnItemCreated;

    public IItem Create(IItemType itemType, Location location)
    {
        return new Item(itemType, location);
    }
}