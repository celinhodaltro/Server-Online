using System.Linq;
using Game.Combat.Conditions;
using Server.Entities.Common.Contracts.Creatures;

namespace Server.Jobs.Creatures;

public static class CreatureConditionJob
{
    public static void Execute(ICombatActor creature)
    {
        if (creature.IsDead) return;

        foreach (var (_, condition) in creature.Conditions.ToList())
        {
            if (condition.HasExpired)
            {
                condition.End();
                creature.RemoveCondition(condition);
            }

            if (condition is DamageCondition damageCondition) damageCondition.Execute(creature);
        }
    }
}