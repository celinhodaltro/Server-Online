﻿using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Item;

namespace Game.Creatures.Events;

public class CreatureKilledEventHandler : IGameEventHandler
{
    private readonly IItemFactory itemFactory;
    private readonly ILiquidPoolFactory liquidPoolFactory;
    private readonly IMap map;

    public CreatureKilledEventHandler(IItemFactory itemFactory, IMap map, ILiquidPoolFactory liquidPoolFactory)
    {
        this.itemFactory = itemFactory;
        this.map = map;
        this.liquidPoolFactory = liquidPoolFactory;
    }

    public void Execute(ICreature creature, IThing by, ILoot loot)
    {
        if (creature is IMonster { IsSummon: true } monster)
        {
            //if summon just remove the creature from map
            map.RemoveCreature(monster);
            return;
        }

        ReplaceCreatureByCorpse(creature, by, loot);
        CreateBlood(creature);
    }

    private void ReplaceCreatureByCorpse(ICreature creature, IThing by, ILoot loot)
    {
        var corpse = itemFactory.CreateLootCorpse(creature.CorpseType, creature.Location, loot);
        creature.Corpse = corpse;

        if (creature is IWalkableCreature walkable)
        {
            walkable.Tile.AddItem(corpse);
            map.RemoveCreature(creature);
        }

        corpse.Decay.StartDecay();
    }

    private void CreateBlood(ICreature creature)
    {
        if (creature is not ICombatActor victim) return;
        var liquidColor = victim.BloodType switch
        {
            BloodType.Blood => LiquidColor.Red,
            BloodType.Slime => LiquidColor.Green,
            _ => LiquidColor.Red
        };

        var pool = liquidPoolFactory.Create(victim.Location, liquidColor);

        map.CreateBloodPool(pool, victim.Tile);
    }
}