using Game.Common.Combat.Structs;
using Game.Common.Contracts;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.World;
using Game.Common.Creatures;
using Game.Common.Item;

namespace Game.Creatures.Events;

public class CreatureDamagedEventHandler : IGameEventHandler
{
    private readonly ILiquidPoolFactory liquidPoolFactory;
    private readonly IMap map;

    public CreatureDamagedEventHandler(IMap map, ILiquidPoolFactory liquidPoolFactory)
    {
        this.map = map;
        this.liquidPoolFactory = liquidPoolFactory;
    }

    public void Execute(IThing enemy, ICreature victim, CombatDamage damage)
    {
        CreateBlood(victim, damage);
    }

    private void CreateBlood(ICreature creature, CombatDamage damage)
    {
        if (creature is not ICombatActor victim) return;

        if (damage.IsElementalDamage) return;

        var liquidColor = victim.BloodType switch
        {
            BloodType.Blood => LiquidColor.Red,
            BloodType.Slime => LiquidColor.Green,
            _ => LiquidColor.Red
        };

        var pool = liquidPoolFactory.CreateDamageLiquidPool(victim.Location, liquidColor);

        map.CreateBloodPool(pool, victim.Tile);
    }
}