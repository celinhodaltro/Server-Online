using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;

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