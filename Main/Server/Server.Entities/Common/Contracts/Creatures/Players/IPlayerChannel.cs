﻿using System.Collections.Generic;
using Server.Entities.Common.Contracts.Chats;
using Server.Entities.Common.Contracts;

namespace Server.Entities.Common.Contracts.Creatures.Players;

public delegate void PlayerJoinChannel(IPlayer player, IChatChannel channel);

public delegate void PlayerExitChannel(IPlayer player, IChatChannel channel);

public interface IPlayerChannel
{
    IEnumerable<IChatChannel> PersonalChannels { get; }
    IEnumerable<IChatChannel> PrivateChannels { get; }
    bool CanEnterOnChannel(ushort channelId, IChatChannelStore chatChannelStore);
    void AddPersonalChannel(IChatChannel channel);
    bool JoinChannel(IChatChannel channel);
    bool ExitChannel(IChatChannel channel);
    bool SendMessage(IChatChannel channel, string message);
    event PlayerJoinChannel OnJoinedChannel;
    event PlayerExitChannel OnExitedChannel;
}