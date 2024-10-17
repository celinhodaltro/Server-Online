using Server.Entities.Common.Contracts.Items;
using Server.Contracts.Contracts;

namespace Server.Events.Items;

public class ItemStartedDecayingEventHandler
{
    private readonly IDecayableItemManager _decayableItemManager;

    public ItemStartedDecayingEventHandler(IDecayableItemManager decayableItemManager)
    {
        _decayableItemManager = decayableItemManager;
    }

    public void Execute(IItem item)
    {
        _decayableItemManager.Add(item);
    }
}