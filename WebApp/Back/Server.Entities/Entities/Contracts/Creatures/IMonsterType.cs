using System.Collections.Generic;
using System.Collections.Immutable;
using Server.Entities.Contracts.Combat;
using Server.Entities.Contracts.Combat.Attacks;
using Server.Entities.Contracts.Creatures.Monsters;
using Server.Entities.Creatures;
using Server.Entities.Item;

namespace Server.Entities.Contracts.Creatures;

public interface IMonsterType : ICreatureType
{
    ushort Armor { get; set; }
    ushort Defense { get; set; }

    public uint Experience { get; set; }
    public IMonsterCombatAttack[] Attacks { get; set; }
    public ICombatDefense[] Defenses { get; set; }

    IDictionary<CreatureFlagAttribute, ushort> Flags { get; set; }
    IIntervalChance TargetChance { get; set; }

    /// <summary>
    ///     Monster's phases
    /// </summary>
    Voice[] Voices { get; set; }

    /// <summary>
    ///     Voice interval and chance to happen
    /// </summary>
    IIntervalChance VoiceConfig { get; set; }

    ImmutableDictionary<DamageType, sbyte> ElementResistance { get; set; }
    Race Race { get; set; }
    ILoot Loot { get; set; }
    IMonsterSummon[] Summons { get; set; }
    byte MaxSummons { get; set; }
    ushort Immunities { get; set; }
    bool HasDistanceAttack { get; set; }
    byte MaxRangeDistanceAttack { get; set; }
    bool HasFlag(CreatureFlagAttribute flag);
}