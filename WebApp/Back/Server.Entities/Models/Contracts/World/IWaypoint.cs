using Game.Common.Location.Structs;

namespace Server.Entities.Models.Contracts.World;

public interface IWaypoint
{
    string Name { get; set; }
    Coordinate Coordinate { get; set; }
}