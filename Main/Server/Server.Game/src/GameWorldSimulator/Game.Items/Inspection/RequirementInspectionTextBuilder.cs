﻿using System.Text;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Helpers;

namespace Game.Items.Inspection;

public static class RequirementInspectionTextBuilder
{
    public static string Build(IItem item, IVocationStore vocationStore)
    {
        if (item is not IRequirement itemRequirement) return string.Empty;

        var vocations = itemRequirement.Vocations;
        var minLevel = itemRequirement.MinLevel;

        if (Guard.IsNullOrEmpty(vocations) && minLevel == 0) return string.Empty;
        var vocationsText = FormatVocations(vocations, vocationStore);

        var verb = itemRequirement switch
        {
            IEquipmentRequirement => "wielded",
            IConsumableRequirement => "consumed",
            IUsableRequirement => "used",
            _ => "wielded"
        };

        return
            $"It can only be {verb} properly by {vocationsText}{(minLevel > 0 ? $" of level {minLevel} or higher" : string.Empty)}.";
    }

    private static string FormatVocations(byte[] allVocations, IVocationStore vocationStore)
    {
        if (Guard.IsNullOrEmpty(allVocations)) return "players";
        var text = new StringBuilder();
        for (var i = 0; i < allVocations.Length; i++)
        {
            if (!vocationStore.TryGetValue(allVocations[i], out var vocation)) continue;
            text.Append($"{vocation.Name.ToLower()}s");
            var lastItem = i == allVocations.Length - 1;
            var penultimate = i == allVocations.Length - 2;
            if (lastItem) continue;
            if (penultimate)
            {
                text.Append(" and ");
                continue;
            }

            text.Append(", ");
        }

        var finalText = text.ToString();
        return string.IsNullOrWhiteSpace(finalText) ? "players" : text.ToString();
    }
}