using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Events.Items;

namespace Server.Events.Subscribers;

public class ItemEventSubscriber : IItemEventSubscriber
{
    private readonly ItemStartedDecayingEventHandler _itemStartedDecayingEventHandler;
    private readonly ItemUsedOnTileEventHandler _itemUsedOnTileEventHandler;

    public ItemEventSubscriber(ItemUsedOnTileEventHandler itemUsedOnTileEventHandler,
        ItemStartedDecayingEventHandler itemStartedDecayingEventHandler)
    {
        _itemUsedOnTileEventHandler = itemUsedOnTileEventHandler;
        _itemStartedDecayingEventHandler = itemStartedDecayingEventHandler;
    }

    public void Subscribe(IItem item)
    {
        if (item is null) return;
        if (item is IUsableOnTile usableOnTile) usableOnTile.OnUsedOnTile += _itemUsedOnTileEventHandler.Execute;
        if (item.HasDecayBehavior)
            item.Decay.OnStarted += _itemStartedDecayingEventHandler.Execute;
    }

    public void Unsubscribe(IItem item)
    {
        if (item is null) return;
        if (item is IUsableOnTile usableOnTile) usableOnTile.OnUsedOnTile -= _itemUsedOnTileEventHandler.Execute;
        if (item.HasDecayBehavior && item is IHasDecay decayItem)
            decayItem.Decay.OnStarted -= _itemStartedDecayingEventHandler.Execute;
    }
}