using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Server.Entities.Common.Contracts.Creatures;

namespace Game.Creatures.Monster.Managers;

public class MonsterDataManager : IMonsterDataManager
{
    private bool _loaded;
    private IImmutableDictionary<string, IMonsterType> _monsters;

    public bool TryGetMonster(string name, out IMonsterType monster)
    {
        return _monsters.TryGetValue(name, out monster);
    }

    public void Load(IEnumerable<(string, IMonsterType)> monsters)
    {
        if (_loaded) throw new InvalidOperationException("Monsters already loaded");

        _monsters = monsters.ToImmutableDictionary(x => x.Item1, v => v.Item2,
            StringComparer.InvariantCultureIgnoreCase);
        _loaded = true;
    }
}