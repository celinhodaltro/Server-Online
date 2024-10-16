﻿using Data.Interfaces;
using Server.Entities.Common.Contracts.Creatures;
using Networking.Packets.Outgoing.Chat;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerAddToVipListEventHandler
{
    private readonly IAccountRepository accountRepository;
    private readonly IGameServer game;

    public PlayerAddToVipListEventHandler(IGameServer game, IAccountRepository accountRepository)
    {
        this.game = game;
        this.accountRepository = accountRepository;
    }

    public async void Execute(IPlayer player, uint vipPlayerId, string vipPlayerName)
    {
        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        await accountRepository.AddPlayerToVipList((int)player.AccountId, (int)vipPlayerId);

        var isOnline = game.CreatureManager.TryGetLoggedPlayer(vipPlayerId, out _);

        connection.OutgoingPackets.Enqueue(new PlayerAddVipPacket(vipPlayerId, vipPlayerName, isOnline));
        connection.Send();
    }
}