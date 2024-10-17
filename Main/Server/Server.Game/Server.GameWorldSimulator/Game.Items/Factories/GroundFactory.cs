using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Location.Structs;
using Game.Items.Items;

namespace Game.Items.Factories;

public class GroundFactory : IFactory
{
    public event CreateItem OnItemCreated;


    public IItem Create(IItemType itemType, Location location)
    {
        if (Ground.IsApplicable(itemType)) return new Ground(itemType, location);

        return null;
    }
}