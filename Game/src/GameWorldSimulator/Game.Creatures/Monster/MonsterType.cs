using System.Collections.Generic;
using System.Collections.Immutable;
using Game.Common.Contracts.Combat;
using Game.Common.Contracts.Combat.Attacks;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Creatures.Monsters;
using Game.Common.Creatures;
using Game.Common.Item;
using Game.Creatures.Monster.Combat;

namespace Game.Creatures.Monster;

public sealed class MonsterType : IMonsterType
{
    public ushort ManaCost { get; set; }
    public CombatStrategy CombatStrategy { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public Race Race { get; set; }
    public uint Experience { get; set; }
    public ushort Speed { get; set; }
    public uint MaxHealth { get; set; }
    public IDictionary<LookType, ushort> Look { get; set; }
    public IIntervalChance TargetChance { get; set; }

    public IDictionary<CreatureFlagAttribute, ushort> Flags { get; set; } =
        new Dictionary<CreatureFlagAttribute, ushort>();

    public bool HasFlag(CreatureFlagAttribute flag)
    {
        return Flags.TryGetValue(flag, out var value) && value > 0;
    }

    public IMonsterCombatAttack[] Attacks { get; set; }
    public ushort Armor { get; set; }
    public ushort Defense { get; set; }
    public ICombatDefense[] Defenses { get; set; }
    public IIntervalChance VoiceConfig { get; set; }
    public Voice[] Voices { get; set; }
    public ImmutableDictionary<DamageType, sbyte> ElementResistance { get; set; }
    public ushort Immunities { get; set; }
    public ILoot Loot { get; set; }
    public byte MaxSummons { get; set; }
    public IMonsterSummon[] Summons { get; set; }
    public bool HasDistanceAttack { get; set; }
    public byte MaxRangeDistanceAttack { get; set; }
}