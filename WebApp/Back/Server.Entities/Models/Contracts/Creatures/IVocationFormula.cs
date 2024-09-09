﻿namespace Server.Entities.Models.Contracts.Creatures;

public interface IVocationFormula
{
    float Armor { get; set; }
    float Defense { get; set; }
    float DistDamage { get; set; }
    float MeleeDamage { get; set; }
}