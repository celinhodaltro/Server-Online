using System;
using System.Linq;
using Game.Common.Contracts;
using Game.Common.Contracts.Chats;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.World;

namespace Game.Creatures.Events.Player;

public class PlayerLoggedInEventHandler : IGameEventHandler
{
    private readonly IChatChannelStore _chatChannelStore;
    private readonly IMap _map;

    public PlayerLoggedInEventHandler(IChatChannelStore chatChannelStore, IMap map)
    {
        _chatChannelStore = chatChannelStore;
        _map = map;
    }

    public void Execute(IPlayer player)
    {
        if (player is null) return;

        _map.PlaceCreature(player);

        JoinChannels(player);
    }

    private void JoinChannels(IPlayer player)
    {
        var channels = _chatChannelStore.All.Where(x => x.Opened);

        channels = player.Channels.PersonalChannels is null
            ? channels
            : channels.Concat(player.Channels.PersonalChannels?.Where(x => x.Opened) ?? Array.Empty<IChatChannel>());

        channels = player.Channels.PrivateChannels is not { } privateChannels
            ? channels
            : channels.Concat(privateChannels.Where(x => x.Opened));

        foreach (var channel in channels) player.Channels.JoinChannel(channel);
    }
}