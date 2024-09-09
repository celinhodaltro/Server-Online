using Server.Entities.Contracts.World;

namespace Server.Entities.Contracts.Creatures;

public interface INpcFactory
{
    INpc Create(string name, ISpawnPoint spawn = null);
}