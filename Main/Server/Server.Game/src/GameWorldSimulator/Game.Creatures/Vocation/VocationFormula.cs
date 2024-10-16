﻿using Server.Entities.Common.Contracts.Creatures;

namespace Game.Creatures.Vocation;

public class VocationFormula : IVocationFormula
{
    public float MeleeDamage { get; set; }
    public float DistDamage { get; set; }
    public float Defense { get; set; }
    public float Armor { get; set; }
}