using FluentAssertions;
using Game.Common.Creatures;
using Game.Tests.Helpers;
using Game.Tests.Helpers.Map;
using Game.Tests.Helpers.Player;
using Game.World.Models.Tiles;
using Xunit;

namespace Game.Creatures.Tests.Monster;

public class MonsterEscapeTests
{
    [Fact]
    public void Monster_stops_attack_and_follow_while_escaping()
    {
        //arrange
        var map = MapTestDataBuilder.Build(100, 101, 100, 101, 7, 7);

        var monster = MonsterTestDataBuilder.Build();
        monster.Metadata.Flags.Add(CreatureFlagAttribute.Hostile, 1);

        var enemy = PlayerTestDataBuilder.Build();
        using var monitor = monster.Monitor();

        (map[100, 100, 7] as DynamicTile)?.AddCreature(monster);
        (map[100, 101, 7] as DynamicTile)?.AddCreature(enemy);

        monster.SetAttackTarget(enemy);
        monster.SetAsEnemy(enemy);

        //act
        monster.Escape();

        //assert
        monitor.Should().Raise(nameof(monster.OnStoppedAttack));
        monster.IsFollowing.Should().BeFalse();
    }
}