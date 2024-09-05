using System;
using System.Collections.Generic;
using FluentAssertions;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Creatures;
using Game.Common.Creatures.Players;
using Game.Common.Item;
using Game.Creatures.Player;
using Game.Tests.Helpers;
using Game.Tests.Helpers.Player;
using Xunit;

namespace Game.Creatures.Tests.Players;

public class PlayerSkillTest
{
    [Fact]
    public void GetSkillLevel_When_Has_No_Skill_Returns_1()
    {
        var player = PlayerTestDataBuilder.Build(hp: 100, skills: new Dictionary<SkillType, ISkill>
        {
            { SkillType.Axe, new Skill(SkillType.Axe, 12) }
        });
        var level = player.GetSkillLevel(SkillType.Club);

        Assert.Equal(1, level);
    }

    [Fact]
    public void GetSkillLevel_When_Has_Skill_Returns_Level()
    {
        var player = PlayerTestDataBuilder.Build(hp: 100, skills: new Dictionary<SkillType, ISkill>
        {
            { SkillType.Axe, new Skill(SkillType.Axe, 12) }
        });
        var level = player.GetSkillLevel(SkillType.Axe);

        Assert.Equal(12, level);
    }

    [Fact]
    public void Player_wearing_a_non_skill_bonus_item_skill_remains_the_same()
    {
        var player = PlayerTestDataBuilder.Build(hp: 100, skills: new Dictionary<SkillType, ISkill>
        {
            { SkillType.Axe, new Skill(SkillType.Axe, 12) }
        }, inventoryMap: new Dictionary<Slot, (IItem Item, ushort Id)>
        {
            {
                Slot.Necklace,
                (ItemTestData.CreateDefenseEquipmentItem(100, "necklace"), 1)
            }
        });
        var level = player.GetSkillLevel(SkillType.Axe);

        Assert.Equal(12, level);
    }

    [Fact]
    public void Player_wearing_a_skill_bonus_item_has_skill_increased()
    {
        //arrange
        var necklace = ItemTestData.CreateDefenseEquipmentItem(100, "necklace",
            attributes: new (ItemAttribute, IConvertible)[]
            {
                (ItemAttribute.SkillAxe, 5)
            });

        var player = PlayerTestDataBuilder.Build(hp: 100, skills: new Dictionary<SkillType, ISkill>
        {
            { SkillType.Axe, new Skill(SkillType.Axe, 12) }
        }, inventoryMap: new Dictionary<Slot, (IItem Item, ushort Id)>
        {
            { Slot.Necklace, (necklace, 1) }
        });

        //act
        var result = player.GetSkillLevel(SkillType.Axe);

        //assert
        result.Should().Be(17);
    }
}