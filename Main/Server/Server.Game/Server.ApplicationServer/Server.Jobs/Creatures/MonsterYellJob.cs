using Server.Entities.Common.Contracts.Creatures;

namespace Server.Jobs.Creatures;

public static class MonsterYellJob
{
    public static void Execute(IMonster monster)
    {
        if (monster.IsDead) return;

        monster.Yell();
    }
}