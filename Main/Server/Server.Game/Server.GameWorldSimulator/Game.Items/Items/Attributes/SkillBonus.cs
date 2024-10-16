using System.Collections.Generic;
using System.Text;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Parsers;

namespace Game.Items.Items.Attributes;

public sealed class SkillBonus : ISkillBonus
{
    private readonly IItem _item;

    public SkillBonus(IItem item)
    {
        _item = item;
    }

    private Dictionary<SkillType, sbyte> SkillBonuses => _item.Metadata.Attributes.SkillBonuses;

    public void AddSkillBonus(IPlayer player)
    {
        if (Guard.AnyNull(SkillBonuses, player)) return;
        foreach (var (skillType, bonus) in SkillBonuses) player.AddSkillBonus(skillType, bonus);
    }

    public void RemoveSkillBonus(IPlayer player)
    {
        if (Guard.AnyNull(SkillBonuses, player)) return;
        foreach (var (skillType, bonus) in SkillBonuses) player.RemoveSkillBonus(skillType, bonus);
    }

    public override string ToString()
    {
        if (Guard.AnyNullOrEmpty(SkillBonuses)) return string.Empty;

        var stringBuilder = new StringBuilder();
        foreach (var (skillType, value) in SkillBonuses)
        {
            if (value == 0) continue;
            stringBuilder.Append($"{SkillTypeParser.Parse(skillType).ToLower()} +{value}, ");
        }

        if (stringBuilder.Length < 2) return string.Empty;

        stringBuilder.Remove(stringBuilder.Length - 2, 2);
        return stringBuilder.ToString();
    }
}