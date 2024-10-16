using Server.Common.Contracts;
using Server.Tasks;

namespace Server.Jobs.Items;

public class GameItemJob
{
    private const ushort EVENT_CHECK_ITEM_INTERVAL = 1000;
    private readonly IGameServer _game;

    public GameItemJob(IGameServer game)
    {
        _game = game;
    }

    public void StartChecking()
    {
        _game.Scheduler.AddEvent(new SchedulerEvent(EVENT_CHECK_ITEM_INTERVAL, StartChecking));

        _game.DecayableItemManager.DecayExpiredItems();
    }
}