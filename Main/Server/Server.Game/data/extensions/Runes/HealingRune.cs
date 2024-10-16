using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Items.UsableItems.Runes;

namespace Extensions.Runes;

public class HealingRune : Rune, IConsumable, IUsableOnCreature, IUsableOn
{
    public HealingRune(IItemType type, Location location, IDictionary<ItemAttribute, IConvertible> attributes) :
        base(type, location, attributes)
    {
    }

    public override ushort Duration => 0;
    public event Use OnUsed;
    public EffectT Effect => EffectT.GlitterBlue;

    public void Use(IPlayer usedBy, ICreature creature)
    {
        var minMax = Formula(usedBy, usedBy.Level, usedBy.GetSkillLevel(SkillType.Magic));
        var healValue = (ushort)GameRandom.Random.Next(minMax.Min, maxValue: minMax.Max);
        if (creature is ICombatActor actor) actor.Heal(healValue, usedBy);

        Reduce();

        OnUsed?.Invoke(usedBy, creature, this);
    }
}