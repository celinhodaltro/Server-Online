﻿using System.Collections.Immutable;
using System.Text;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items.Types.Body;

public interface IInventoryEquipment : IEquipmentRequirement, IMovableThing
{
    public Slot Slot => Metadata.BodyPosition;
}

public interface IBodyEquipmentEquipment : IDressable, IInventoryEquipment
{
    bool Pickupable => true;

    ushort MinimumLevelRequired => Metadata.Attributes.GetAttribute<ushort>(ItemAttribute.MinimumLevel);

    public ImmutableDictionary<SkillType, sbyte> SkillBonus =>
        Metadata.Attributes.SkillBonuses.ToImmutableDictionary();

    public WeaponType WeaponType => Metadata.WeaponType;

    protected string RequirementText
    {
        get
        {
            var stringBuilder = new StringBuilder();
            var sufix = "\nIt can only be wielded properly by";
            //todo: add vocations 
            if (MinLevel > 0) stringBuilder.Append($" of level {MinLevel} or higher");

            return $"{sufix} {stringBuilder}";
        }
    }
}