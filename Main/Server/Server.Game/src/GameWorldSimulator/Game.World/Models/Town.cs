using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Location.Structs;

namespace Game.World.Models;

public struct Town : ITown
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public Coordinate Coordinate { get; set; }
}