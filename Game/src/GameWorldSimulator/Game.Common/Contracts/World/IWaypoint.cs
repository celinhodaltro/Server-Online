using Game.Common.Location.Structs;

namespace Game.Common.Contracts.World;

public interface IWaypoint
{
    string Name { get; set; }
    Coordinate Coordinate { get; set; }
}