using System;
using System.Collections.Generic;
using System.Globalization;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Inspection;
using Game.Common.Contracts.Items;
using Game.Common.Helpers;
using Game.Common.Item;
using Game.Common.Location.Structs;
using Game.Items.Bases;

namespace Game.Items.Items;

public class Sign : BaseItem, ISign
{
    public Sign(IItemType metadata, Location location, IDictionary<ItemAttribute, IConvertible> attributes) : base(
        metadata, location)
    {
        attributes.TryGetValue(ItemAttribute.Text, out var text);
        Text = text?.ToString(CultureInfo.InvariantCulture);
    }

    public string Text { get; }

    public override string GetLookText(IInspectionTextBuilder inspectionTextBuilder, IPlayer player,
        bool isClose = false)
    {
        var lookText = base.GetLookText(inspectionTextBuilder, player, isClose);

        return string.IsNullOrWhiteSpace(Text) ? lookText : $"{lookText}\nYou read: {Text.AddEndOfSentencePeriod()}";
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.Paper;
    }
}