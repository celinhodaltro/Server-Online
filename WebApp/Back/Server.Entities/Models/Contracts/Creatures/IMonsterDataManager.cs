using System.Collections.Generic;

namespace Server.Entities.Models.Contracts.Creatures;

public interface IMonsterDataManager
{
    void Load(IEnumerable<(string, IMonsterType)> monsters);
    bool TryGetMonster(string name, out IMonsterType monster);
}