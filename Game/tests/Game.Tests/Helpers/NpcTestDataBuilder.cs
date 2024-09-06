using Moq;
using Data.InMemory;
using Game.Common.Contracts.Creatures;
using Game.Common.Location.Structs;
using Game.Creatures.Factories;
using Game.Items.Factories;
using Game.Tests.Helpers.Map;
using Game.World.Models.Spawns;
using Game.World.Services;
using Serilog;
using PathFinder = Game.World.Map.PathFinder;

namespace Game.Tests.Helpers;

public static class NpcTestDataBuilder
{
    public static INpc Build(string name, INpcType npcType)
    {
        var logger = new Mock<ILogger>();
        var itemFactory = new ItemFactory();

        var npcStore = new NpcStore();
        npcStore.Add(name, npcType);

        var coinTypeStore = new CoinTypeStore();

        var map = MapTestDataBuilder.Build(100, 110, 100, 110, 7, 7);
        var pathFinder = new PathFinder(map);
        var mapTool = new MapTool(map, pathFinder);

        var spawnPoint = new SpawnPoint(new Location(105, 105, 7), 60);

        var npcFactory = new NpcFactory(logger.Object, itemFactory, npcStore, coinTypeStore, mapTool);

        var npc = npcFactory.Create(name, spawnPoint);

        npc.SetNewLocation(new Location(105, 105, 7));
        map.PlaceCreature(npc);

        return npc;
    }
}