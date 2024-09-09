using Server.Entities.Models.Contracts.World;
using Server.Entities.Models.Location;

namespace Server.Entities.Models.World;

public class Spawn : ISpawn
{
    public byte Radius { get; set; }
    public Location.Structs.Location Location { get; set; }
    public ISpawn.ICreature[] Monsters { get; set; }
    public ISpawn.ICreature[] Npcs { get; set; }

    public class Creature : ISpawn.ICreature
    {
        public string Name { get; set; }
        public ISpawnPoint Spawn { get; set; }
    }
}

public class SpawnPoint : ISpawnPoint
{
    public SpawnPoint(Location.Structs.Location location, ushort spawnTime, Direction direction = Direction.North)
    {
        Location = location;
        SpawnTime = spawnTime;
        Direction = direction;
    }

    public Location.Structs.Location Location { get; }
    public ushort SpawnTime { get; }
    public Direction Direction { get; set; }
}