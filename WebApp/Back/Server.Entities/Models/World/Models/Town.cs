using Server.Entities.Models.Contracts.World;
using Server.Entities.Models.Location.Structs;

namespace Server.Entities.Models.World;

public struct Town : ITown
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public Coordinate Coordinate { get; set; }
}