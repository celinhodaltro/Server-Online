using Server.Entities.Common.Location.Structs;

namespace Server.Entities.Common.Contracts.World;

public interface IWaypoint
{
    string Name { get; set; }
    Coordinate Coordinate { get; set; }
}