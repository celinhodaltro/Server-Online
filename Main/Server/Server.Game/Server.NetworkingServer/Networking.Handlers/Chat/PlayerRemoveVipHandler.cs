﻿using Data.Interfaces;
using Networking.Packets.Incoming.Chat;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Chat;

public class PlayerRemoveVipHandler : PacketHandler
{
    private readonly IAccountRepository _accountRepository;
    private readonly IGameServer _game;

    public PlayerRemoveVipHandler(IGameServer game, IAccountRepository accountRepository)
    {
        _game = game;
        _accountRepository = accountRepository;
    }

    public override async void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        var removeVipPacket = new RemoveVipPacket(message);

        _game.Dispatcher.AddEvent(new Event(() => player.Vip.RemoveFromVip(removeVipPacket.PlayerId)));

        await _accountRepository.RemoveFromVipList((int)player.AccountId, (int)removeVipPacket.PlayerId);
    }
}