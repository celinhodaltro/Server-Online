using Game.Common.Location;
using Game.Common.Location.Structs;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Inspection;
using Server.Entities.Models.Contracts.Items.Types.Usable;

namespace Server.Entities.Models.Contracts.Items;

public interface IThing : IUsable
{
    string Name { get; }

    public byte Amount => 1;

    Location Location { get; }

    string GetLookText(IInspectionTextBuilder inspectionTextBuilder, IPlayer player, bool isClose = false);

    public bool IsCloseTo(IThing thing)
    {
        if (Location.Type is not LocationType.Ground &&
            this is IItem { CanBeMoved: true } item)
            return item.Owner?.Location.IsNextTo(thing.Location) ?? false;
        return Location.IsNextTo(thing.Location);
    }

    void SetNewLocation(Location location);
}