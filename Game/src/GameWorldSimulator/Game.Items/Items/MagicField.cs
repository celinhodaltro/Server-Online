using System;
using Game.Combat.Conditions;
using Server.Entities.Common;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Effects.Parsers;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Parsers;
using Game.Items.Bases;

namespace Game.Items.Items;

public class MagicField : BaseItem, IMagicField
{
    public MagicField(IItemType type, Location location) : base(type, location)
    {
    }

    private byte DamageCount => Metadata.Attributes.GetInnerAttributes(ItemAttribute.Field)
        ?.GetAttribute<byte>(ItemAttribute.Count) ?? 0;

    private DamageType DamageType => DamageTypeParser.Parse(Metadata.Attributes.GetAttribute(ItemAttribute.Field));

    private int Interval =>
        Metadata.Attributes.GetInnerAttributes(ItemAttribute.Field)?.GetAttribute<int>(ItemAttribute.Ticks) ??
        10000;

    private MinMax Damage
    {
        get
        {
            var attributes = Metadata.Attributes.GetInnerAttributes(ItemAttribute.Field);
            if (attributes is null) return new MinMax();

            var values = attributes.GetAttributeArray(ItemAttribute.Damage);

            if ((values?.Length ?? 0) < 2) return new MinMax(0, 0);

            int.TryParse((string)values[0], out var value1);
            int.TryParse((string)values[1], out var value2);

            return new MinMax(Math.Min(value1, value2), Math.Max(value1, value2));
        }
    }

    public void CauseDamage(ICreature toCreature)
    {
        if (toCreature is not ICombatActor actor) return;

        var damages = Damage;

        if (damages.Max == 0) return;
        var conditionType = ConditionTypeParser.Parse(DamageType);
        actor.ReceiveAttack(this,
            new CombatDamage((ushort)damages.Max, DamageType) { Effect = DamageEffectParser.Parse(DamageType) });

        if (actor.HasCondition(conditionType, out var condition) && condition is DamageCondition damageCondition)
        {
            if (DamageCount == 0) damageCondition.Start(toCreature, (ushort)damages.Min, (ushort)damages.Max);
            else damageCondition.Restart(DamageCount);
        }
        else
        {
            if (DamageCount == 0)
                actor.AddCondition(new DamageCondition(conditionType, Interval, (ushort)damages.Min,
                    (ushort)damages.Max));
            else
                actor.AddCondition(new DamageCondition(conditionType, Interval, DamageCount, (ushort)damages.Min));
        }
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.MagicField;
    }
}