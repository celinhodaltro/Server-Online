using System;
using System.Collections.Generic;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Location.Structs;
using Game.Creatures.Player;
using Server.Entities.Common.Characters;

namespace Extensions.Players;

public class Tutor : Player
{
    public Tutor(uint id, string characterName, IVocation vocation, Gender gender, bool online,
        IDictionary<SkillType, ISkill> skills, IOutfit outfit, ushort speed, Location location,
        IMapTool mapTool, ITown town) :
        base(id, characterName, ChaseMode.Follow, ushort.MaxValue, ushort.MaxValue, ushort.MaxValue, vocation,
            gender, online, ushort.MaxValue, ushort.MaxValue, FightMode.Balanced, 100, 100, skills, 60, outfit,
            speed, location, mapTool, town)
    {
    }

    public override bool CanBeAttacked => false;

    public override void GainExperience(long exp)
    {
    } //tutor do not gain experience

    public override void LoseExperience(long exp)
    {
        Console.WriteLine("tutor do not lose experience");
    }

    public override void OnDamage(IThing enemy, CombatDamage damage)
    {
    }

    public override void OnAppear(Location location, ICylinderSpectator[] spectators)
    {
    }
}