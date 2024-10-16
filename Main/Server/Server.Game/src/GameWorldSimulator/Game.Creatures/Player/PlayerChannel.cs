﻿using System.Collections.Generic;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Services;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Creatures.Players;

namespace Game.Creatures.Player;

public class PlayerChannel : IPlayerChannel
{
    private readonly IPlayer _owner;

    private IDictionary<ushort, IChatChannel> _personalChannels;

    public PlayerChannel(IPlayer owner)
    {
        _owner = owner;
    }

    private uint CreatureId => _owner.CreatureId;

    public IEnumerable<IChatChannel> PersonalChannels => _personalChannels?.Values;

    public bool CanEnterOnChannel(ushort channelId, IChatChannelStore chatChannelStore)
    {
        var channel = chatChannelStore.Get(channelId);
        return channel?.PlayerCanJoin(_owner) ?? false;
    }

    public IEnumerable<IChatChannel> PrivateChannels
    {
        get
        {
            if (_owner.HasGuild) yield return _owner.Guild?.Channel;
            if (_owner.PlayerParty.Party?.Channel is not null) yield return _owner.PlayerParty.Party.Channel;
        }
    }

    public void AddPersonalChannel(IChatChannel channel)
    {
        if (Guard.IsNull(channel)) return;

        _personalChannels ??= new Dictionary<ushort, IChatChannel>();
        _personalChannels.Add(channel.Id, channel);
    }

    public bool JoinChannel(IChatChannel channel)
    {
        if (channel is null) return false;

        if (channel.HasUser(_owner))
        {
            OperationFailService.Send(CreatureId, "You've already joined this chat channel");
            return false;
        }

        if (!channel.AddUser(_owner))
        {
            OperationFailService.Send(CreatureId, "You cannot join this chat channel");
            return false;
        }

        OnJoinedChannel?.Invoke(_owner, channel);
        return true;
    }

    public bool ExitChannel(IChatChannel channel)
    {
        if (channel is null) return false;

        if (!channel.HasUser(_owner)) return false;
        if (!channel.RemoveUser(_owner))
        {
            OperationFailService.Send(CreatureId, "You cannot exit this chat channel");
            return false;
        }

        OnExitedChannel?.Invoke(_owner, channel);
        return true;
    }

    public bool SendMessage(IChatChannel channel, string message)
    {
        if (!channel.WriteMessage(_owner, message, out var cancelMessage))
        {
            OperationFailService.Send(CreatureId, cancelMessage);
            return false;
        }

        return true;
    }

    public event PlayerJoinChannel OnJoinedChannel;
    public event PlayerExitChannel OnExitedChannel;
}