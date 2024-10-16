﻿using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;

namespace Game.Creatures.Vocation;

public class Vocation : IVocation
{
    public static float DefaultSkillMultiplier = 4;
    public string Id { get; set; }
    public string Clientid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ushort GainCap { get; set; }
    public ushort GainHp { get; set; }
    public ushort GainMana { get; set; }
    public byte GainHpTicks { get; set; }
    public byte GainManaTicks { get; set; }
    public ushort GainHpAmount { get; set; }
    public ushort GainManaAmount { get; set; }
    public ushort AttackSpeed { get; set; }
    public string Inspect { get; set; }
    public ushort BaseSpeed { get; set; }
    public byte SoulMax { get; set; }
    public byte GainSoulTicks { get; set; }
    public string FromVoc { get; set; }
    public IVocationFormula Formula { get; set; }
    public Dictionary<SkillType, float> Skills { get; set; }
    public byte VocationType => byte.Parse(Id);
}