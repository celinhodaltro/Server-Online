using Server.Entities.Models.Contracts.World;
using Server.Entities.Models.Location.Structs;

namespace Server.Entities.Models.World;

public struct Waypoint : IWaypoint
{
    public string Name { get; set; }
    public Coordinate Coordinate { get; set; }
}