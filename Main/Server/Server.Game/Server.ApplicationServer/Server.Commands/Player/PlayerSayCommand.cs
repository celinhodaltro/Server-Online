﻿using System.Linq;
using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Networking.Packets.Incoming;
using Networking.Packets.Outgoing;
using Server.Common.Contracts;
using Server.Common.Contracts.Commands;
using Server.Common.Contracts.Network;

namespace Server.Commands.Player;

public class PlayerSayCommand : ICommand
{
    private readonly IChatChannelStore _chatChannelStore;
    private readonly IGameServer _game;

    public PlayerSayCommand(IGameServer game, IChatChannelStore chatChannelStore)
    {
        _game = game;
        _chatChannelStore = chatChannelStore;
    }

    public void Execute(IPlayer player, IConnection connection, PlayerSayPacket playerSayPacket)
    {
        if (string.IsNullOrWhiteSpace(playerSayPacket.Message) ||
            (playerSayPacket.Message?.Length ?? 0) > 255) return;
        if ((playerSayPacket.Receiver?.Length ?? 0) > 30) return;

        var message = playerSayPacket.Message?.Trim();

        if (player.CastSpell(message)) return;

        switch (playerSayPacket.TalkType)
        {
            case SpeechType.None:
                break;
            case SpeechType.Say:
                player.Say(playerSayPacket.Message, playerSayPacket.TalkType);
                break;
            case SpeechType.Whisper:
                break;
            case SpeechType.Yell:
                break;
            case SpeechType.PrivatePlayerToNpc:
                SendMessageToNpc(player, playerSayPacket, message);
                break;
            case SpeechType.PrivateNpcToPlayer:
                break;

            case SpeechType.ChannelOrangeText:
            case SpeechType.ChannelRed1Text:
            case SpeechType.ChannelYellowText:
                SendMessageToChannel(player, playerSayPacket.ChannelId, message);
                break;

            case SpeechType.ChannelRed2Text:
                break;
            case SpeechType.ChannelWhiteText:
                break;
            case SpeechType.RvrChannel:
                break;
            case SpeechType.RvrAnswer:
                break;
            case SpeechType.RvrContinue:
                break;
            case SpeechType.Broadcast:
                break;
            case SpeechType.Private:
            case SpeechType.PrivateRed:
                SendMessageToPlayer(player, connection, playerSayPacket, message);
                break;
            case SpeechType.MonsterSay:
                break;
            case SpeechType.MonsterYell:
                break;
        }
    }

    private void SendMessageToPlayer(IPlayer player, IConnection connection, PlayerSayPacket playerSayPacket,
        string message)
    {
        if (string.IsNullOrWhiteSpace(playerSayPacket.Receiver) ||
            !_game.CreatureManager.TryGetPlayer(playerSayPacket.Receiver, out var receiver))
        {
            connection.OutgoingPackets.Enqueue(new TextMessagePacket("A player with this name is not online.",
                TextMessageOutgoingType.Small));
            connection.Send();
            return;
        }

        player.SendMessageTo(receiver, playerSayPacket.TalkType, message);
    }

    private void SendMessageToNpc(IPlayer player, PlayerSayPacket playerSayPacket, string message)
    {
        foreach (var creature in _game.Map.GetCreaturesAtPositionZone(player.Location))
            if (creature is INpc npc)
            {
                npc.Hear(player, playerSayPacket.TalkType, message);
                return;
            }
    }

    private void SendMessageToChannel(IPlayer player, ushort channelId, string message)
    {
        var channel = _chatChannelStore.Get(channelId);

        if (channel is null) channel = player.Channels.PrivateChannels.FirstOrDefault(x => x.Id == channelId);

        if (channel is null) return;

        player.Channels.SendMessage(channel, message);
    }
}