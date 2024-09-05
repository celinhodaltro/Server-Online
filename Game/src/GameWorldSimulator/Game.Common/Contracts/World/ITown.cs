using Game.Common.Location.Structs;

namespace Game.Common.Contracts.World;

public interface ITown
{
    uint Id { get; set; }
    string Name { get; set; }
    Coordinate Coordinate { get; set; }
}