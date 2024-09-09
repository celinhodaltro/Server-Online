using Game.Common.Contracts.World;

namespace Game.Common.Contracts.Creatures;

public interface INpcFactory
{
    INpc Create(string name, ISpawnPoint spawn = null);
}