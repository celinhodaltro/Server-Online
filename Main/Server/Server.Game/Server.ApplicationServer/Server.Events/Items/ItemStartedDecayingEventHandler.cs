using Server.Entities.Common.Contracts.Items;
using Server.Common.Contracts;

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