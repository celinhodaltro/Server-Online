using Server.Entities.Contracts.World;

namespace Server.Entities.Contracts.Creatures;

public interface IMonsterFactory
{
    IMonster Create(string name, ISpawnPoint spawn = null);
    IMonster CreateSummon(string name, IMonster master);
}