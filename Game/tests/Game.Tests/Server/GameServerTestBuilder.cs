using System;
using Moq;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.World;
using Server;
using Server.Common.Contracts;
using Server.Managers;
using Server.Tasks;
using Serilog;

namespace Game.Tests.Server;

public static class GameServerTestBuilder
{
    public static IGameServer Build(IMap map)
    {
        var logger = new Mock<ILogger>().Object;
        var dispatcher = new Dispatcher(logger);

        var itemTypeStore = ItemTypeStoreTestBuilder.Build(Array.Empty<IItemType>());
        var decayableItemManager = DecayableItemManagerTestBuilder.Build(map, itemTypeStore);
        var persistenceDispatcher = new PersistenceDispatcher(logger);

        var gameServer = new GameServer(map, dispatcher, new OptimizedScheduler(dispatcher),
            new GameCreatureManager(new Mock<ICreatureGameInstance>().Object, map, logger), decayableItemManager,
            persistenceDispatcher);

        return gameServer;
    }
}