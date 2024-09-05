using Game.Common.Combat.Structs;
using Game.Common.Contracts;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.World;

namespace Game.Creatures.Events;

public class CreaturePropagatedAttackEventHandler : IGameEventHandler
{
    private readonly IMap map;

    public CreaturePropagatedAttackEventHandler(IMap map)
    {
        this.map = map;
    }

    public void Execute(ICombatActor actor, CombatDamage damage, AffectedLocation[] area)
    {
        map.PropagateAttack(actor, damage, area);
    }
}