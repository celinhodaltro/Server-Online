using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.World;
using Game.Items.Services;
using Game.Items.Services.ItemTransform;
using Game.World.Services;
using Server.Managers;

namespace Game.Tests.Server;

public class DecayableItemManagerTestBuilder
{
    public static DecayableItemManager Build(IMap map, IItemTypeStore itemTypeStore)
    {
        var mapService = new MapService(map);
        var itemFactory = ItemFactoryTestBuilder.Build();
        var itemTransformService = new ItemTransformService(itemFactory, map, mapService, itemTypeStore);
        var decayService = new DecayService(itemTransformService);
        return new DecayableItemManager(decayService);
    }
}