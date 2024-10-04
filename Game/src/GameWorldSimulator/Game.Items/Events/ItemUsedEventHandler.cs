using Server.Entities.Common;
using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;
using Server.Entities.Common.Results;

namespace Game.Items.Events;

public class ItemUsedEventHandler : IGameEventHandler
{
    private readonly IItemFactory itemFactory;
    private readonly IMap map;

    public ItemUsedEventHandler(IMap map, IItemFactory itemFactory)
    {
        this.map = map;
        this.itemFactory = itemFactory;
    }

    public void Execute(ICreature usedBy, ICreature creature, IItem item)
    {
        Transform(usedBy, creature, item);
        Say(creature, item);
    }

    private void Transform(ICreature usedBy, ICreature creature, IItem item)
    {
        if (item?.CanTransformTo == 0) return;
        if (usedBy is not IPlayer player) return;
        var createdItem = itemFactory.Create(item.CanTransformTo, creature.Location, null);

        if (map[creature.Location] is not IDynamicTile tile) return;

        if (item?.Location.Type == LocationType.Ground) tile.AddItem(createdItem);
        if (item?.Location.Type == LocationType.Container)
        {
            var container = player.Containers[item.Location.ContainerId] ?? player.Inventory?.BackpackSlot;

            var result = container?.AddItem(createdItem) ??
                         new Result<OperationResultList<IItem>>(InvalidOperation.NotPossible);
            if (!result.Succeeded) tile.AddItem(createdItem);
        }
    }

    private void Say(ICreature creature, IItem item)
    {
        if (item is IConsumable consumable && !string.IsNullOrWhiteSpace(consumable.Sentence))
            creature.Say(consumable.Sentence, SpeechType.MonsterSay);
    }
}