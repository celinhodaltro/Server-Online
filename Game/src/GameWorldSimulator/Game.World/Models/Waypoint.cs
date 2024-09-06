using Game.Common.Contracts.World;
using Game.Common.Location.Structs;

namespace Game.World.Models;

public struct Waypoint : IWaypoint
{
    public string Name { get; set; }
    public Coordinate Coordinate { get; set; }
}