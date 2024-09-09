using Server.Entities.Models.Contracts.World;

namespace Server.Entities.Models.Contracts.Creatures;

public interface INpcFactory
{
    INpc Create(string name, ISpawnPoint spawn = null);
}