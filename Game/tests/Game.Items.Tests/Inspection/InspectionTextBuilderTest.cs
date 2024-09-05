using System.Linq;
using FluentAssertions;
using Data.InMemory;
using Game.Common.Item;
using Game.Creatures.Vocation;
using Game.Items.Inspection;
using Game.Tests.Helpers;
using Game.Tests.Helpers.Player;
using Xunit;

namespace Game.Items.Tests.Inspection;

public class InspectionTextBuilderTest
{
    [Theory]
    [InlineData("You see  item.")]
    [InlineData("You see  item.\nIt can only be wielded properly by knights and paladins.", 1, 2)]
    [InlineData("You see  item.\nIt can only be wielded properly by knights.", 1)]
    [InlineData("You see  item.\nIt can only be wielded properly by knights, paladins and sorcerers.", 1, 2, 3)]
    [InlineData("You see  item.\nIt can only be wielded properly by knights, paladins, sorcerers and druids.", 1, 2, 3,
        4)]
    public void Add_HasVocations_ReturnText(string expected, params int[] vocations)
    {
        var vocationStore = new VocationStore();
        vocationStore.Add(1, new Vocation { Name = "Knight" });
        vocationStore.Add(2, new Vocation { Name = "Paladin" });
        vocationStore.Add(3, new Vocation { Name = "Sorcerer" });
        vocationStore.Add(4, new Vocation { Name = "Druid" });

        var input = vocations.Select(x => (byte)x).ToArray();

        var item = ItemTestData.CreateDefenseEquipmentItem(1);
        item.Metadata.Attributes.SetAttribute(ItemAttribute.Vocation, input);

        var player = PlayerTestDataBuilder.Build(hp: 200);
        var inspectionTextBuilder = new InspectionTextBuilder(vocationStore);

        //act
        var actual = inspectionTextBuilder.Build(item, player);

        //assert
        actual.Should().Be(expected);
    }
}