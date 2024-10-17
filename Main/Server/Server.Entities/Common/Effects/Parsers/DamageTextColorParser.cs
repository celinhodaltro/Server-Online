﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Item;
using Server.Entities.Common.Texts;

namespace Server.Entities.Common.Effects.Parsers;

public static class DamageTextColorParser
{
    public static TextColor Parse(DamageType damageType)
    {
        return damageType switch
        {
            DamageType.Fire => TextColor.Orange,
            DamageType.Energy => TextColor.Purple,
            DamageType.Melee => TextColor.Red,
            DamageType.Physical => TextColor.Red,
            DamageType.MagicalPhysical => TextColor.Red,
            DamageType.ManaDrain => TextColor.Blue,
            DamageType.FireField => TextColor.Orange,
            DamageType.Earth => TextColor.LightGreen,
            DamageType.Death => TextColor.DarkRed,
            DamageType.LifeDrain => TextColor.DarkRed,
            DamageType.Ice => TextColor.LightBlue,
            DamageType.Holy => TextColor.Yellow,
            _ => TextColor.Red
        };
    }

    public static TextColor Parse(DamageType damageType, ICreature creature)
    {
        if (damageType is DamageType.Melee && creature is IMonster monster)
            return monster.Metadata.Race switch
            {
                Race.Venom => TextColor.LightGreen,
                Race.Fire => TextColor.Orange,
                Race.Undead => TextColor.Grey,
                Race.Energy => TextColor.ElectricPurple,
                _ => TextColor.Red
            };

        return Parse(damageType);
    }
}