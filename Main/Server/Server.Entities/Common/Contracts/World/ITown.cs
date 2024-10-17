using Server.Entities.Common.Location.Structs;

namespace Server.Entities.Common.Contracts.World;

public interface ITown
{
    uint Id { get; set; }
    string Name { get; set; }
    Coordinate Coordinate { get; set; }
}