using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Inspection;
using Game.Common.Contracts.Items.Types.Usable;
using Game.Common.Location;

namespace Game.Common.Contracts.Items;

public interface IThing : IUsable
{
    string Name { get; }

    public byte Amount => 1;

    Location.Structs.Location Location { get; }

    string GetLookText(IInspectionTextBuilder inspectionTextBuilder, IPlayer player, bool isClose = false);

    public bool IsCloseTo(IThing thing)
    {
        if (Location.Type is not LocationType.Ground &&
            this is IItem { CanBeMoved: true } item)
            return item.Owner?.Location.IsNextTo(thing.Location) ?? false;
        return Location.IsNextTo(thing.Location);
    }

    void SetNewLocation(Location.Structs.Location location);
}