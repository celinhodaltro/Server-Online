﻿using Data.Interfaces;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Networking.Packets.Outgoing.Login;
using Server.Common.Contracts;
using Server.Tasks;

namespace Server.Events.Creature;

public class CreatureKilledEventHandler
{
    private readonly IGameServer _game;
    private readonly IPlayerRepository _playerRepository;

    public CreatureKilledEventHandler(IGameServer game, IPlayerRepository playerRepository)
    {
        _game = game;
        _playerRepository = playerRepository;
    }

    public void Execute(ICombatActor creature, IThing by, ILoot loot)
    {
        _game.Scheduler.AddEvent(new SchedulerEvent(200, () =>
        {
            //send packets to killed player
            if (creature is not IPlayer player ||
                !_game.CreatureManager.GetPlayerConnection(creature.CreatureId, out var connection)) return;

            _playerRepository.SavePlayer(player);

            connection.OutgoingPackets.Enqueue(new ReLoginWindowOutgoingPacket());
            connection.Send();
        }));

        OnMonsterKilled(creature);
    }

    private void OnMonsterKilled(ICombatActor creature)
    {
        if (creature is not IMonster { IsSummon: false } monster) return;

        _game.CreatureManager.AddKilledMonsters(monster);
    }
}