using Game.Common.Contracts.World;

namespace Game.Common.Contracts.Creatures;

public interface IMonsterFactory
{
    IMonster Create(string name, ISpawnPoint spawn = null);
    IMonster CreateSummon(string name, IMonster master);
}