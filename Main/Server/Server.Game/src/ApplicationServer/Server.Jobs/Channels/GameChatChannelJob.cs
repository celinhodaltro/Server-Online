﻿using Server.Entities.Common.Contracts;
using Server.Common.Contracts;
using Server.Tasks;

namespace Server.Jobs.Channels;

public class GameChatChannelJob
{
    private const ushort EVENT_CHECK_ITEM_INTERVAL = 10000;
    private readonly IChatChannelStore _chatChannelStore;
    private readonly IGameServer _game;

    public GameChatChannelJob(IGameServer game, IChatChannelStore chatChannelStore)
    {
        _game = game;
        _chatChannelStore = chatChannelStore;
    }

    public void StartChecking()
    {
        _game.Scheduler.AddEvent(new SchedulerEvent(EVENT_CHECK_ITEM_INTERVAL, StartChecking));

        foreach (var channel in _chatChannelStore.All) ChatUserCleanupJob.Execute(channel);
    }
}