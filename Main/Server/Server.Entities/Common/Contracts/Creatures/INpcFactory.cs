using Server.Entities.Common.Contracts.World;

namespace Server.Entities.Common.Contracts.Creatures;

public interface INpcFactory
{
    INpc Create(string name, ISpawnPoint spawn = null);
}