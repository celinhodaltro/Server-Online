using Server.Entities.Common.Contracts.World;

namespace Server.Entities.Common.Contracts.Creatures;

public interface IMonsterFactory
{
    IMonster Create(string name, ISpawnPoint spawn = null);
    IMonster CreateSummon(string name, IMonster master);
}