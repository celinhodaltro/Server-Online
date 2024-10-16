﻿using Server.Entities.Common.Item;

namespace Server.Entities.Common.Parsers;

public static class WeaponTypeParser
{
    public static WeaponType Parse(string weaponType)
    {
        return weaponType?.ToLower() switch
        {
            "shield" => WeaponType.Shield,
            "ammunition" => WeaponType.Ammunition,
            "axe" => WeaponType.Axe,
            "club" => WeaponType.Club,
            "sword" => WeaponType.Sword,
            "distance" => WeaponType.Distance,
            "wand" => WeaponType.Magical,
            "rod" => WeaponType.Magical,
            _ => WeaponType.None
        };
    }
}