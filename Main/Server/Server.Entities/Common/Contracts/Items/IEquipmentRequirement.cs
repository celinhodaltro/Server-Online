﻿using System.Linq;
using System.Text;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items;

public interface IRequirement : IItem
{
    public byte[] Vocations => Metadata.Attributes.GetRequiredVocations();
    public ushort MinLevel => Metadata.Attributes.GetAttribute<ushort>(ItemAttribute.MinimumLevel);
}

public interface IConsumableRequirement : IRequirement
{
}

public interface IUsableRequirement : IRequirement
{
}

public interface IEquipmentRequirement : IRequirement
{
    public string ValidationError
    {
        get
        {
            var text = new StringBuilder();
            text.Append("Only ");
            //
            // for (int i = 0; i < Vocations.Length; i++)
            // {
            //     text.Append($"{VocationStore.Parse(Vocations[i]).ToLower()}s");
            //     if (i + 1 < Vocations.Length)
            //     {
            //         text.Append(", ");
            //     }
            // }
            text.Append($" of level {MinLevel} or above may use or consume this item");
            return text.ToString();
        }
    }

    public bool CanBeDressed(IPlayer player);

    public bool CanBeUsed(IPlayer player)
    {
        var vocations = Vocations;
        if (vocations?.Length > 0)
            if (!vocations.Contains(player.Vocation.VocationType))
                return false;
        if (MinLevel > 0)
            if (player.Level < MinLevel)
                return false;
        return true;
    }
}