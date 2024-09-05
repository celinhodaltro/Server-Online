using FluentAssertions;
using Game.Common.Location.Structs;
using Game.Systems.Services;
using Game.Tests.Helpers;
using Game.Tests.Helpers.Map;
using Game.Tests.Helpers.Player;
using Game.Tests.Server;
using Game.World.Models.Tiles;
using Server.Commands.Movements;
using Xunit;

namespace Game.Creatures.Tests.Players;

public class PlayerUse
{
    [Fact]
    public void Player_uses_food_when_close_to_it()
    {
        //arrange
        var player = PlayerTestDataBuilder.Build();

        var tile = (DynamicTile)MapTestDataBuilder.CreateTile(new Location(100, 100, 7));
        var secondTile = (DynamicTile)MapTestDataBuilder.CreateTile(new Location(101, 100, 7));

        var map = MapTestDataBuilder.Build(tile, secondTile);

        var food = ItemTestData.CreateFood(2);

        tile.AddCreature(player);
        secondTile.AddItem(food);

        var playerUseService = new PlayerUseService(new WalkToMechanism(GameServerTestBuilder.Build(map)), map);

        //act
        playerUseService.Use(player, food, player);

        //assert
        secondTile.DownItems.Should().BeEmpty();
    }
}