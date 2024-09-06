using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Services;
using Game.Creatures.Monster.Managers;

namespace Server.Jobs.Creatures;

public static class MonsterStateJob
{
    public static void Execute(IMonster monster, ISummonService summonService)
    {
        MonsterStateManager.Run(monster, summonService);
    }
}