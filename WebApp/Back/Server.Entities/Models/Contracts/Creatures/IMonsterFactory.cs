using Server.Entities.Models.Contracts.World;

namespace Server.Entities.Models.Contracts.Creatures;

public interface IMonsterFactory
{
    IMonster Create(string name, ISpawnPoint spawn = null);
    IMonster CreateSummon(string name, IMonster master);
}