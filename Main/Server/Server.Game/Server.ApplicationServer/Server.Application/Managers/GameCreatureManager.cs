﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Serilog;

namespace Server.Managers;

/// <summary>
///     Control creatures on game
/// </summary>
public class GameCreatureManager : IGameCreatureManager
{
    private readonly ICreatureGameInstance _creatureInstances;
    private readonly ILogger _logger;
    private readonly IMap _map;

    private readonly ConcurrentDictionary<uint, IConnection> _playersConnection;

    public GameCreatureManager(ICreatureGameInstance creatureInstances, IMap map, ILogger logger)
    {
        _creatureInstances = creatureInstances;
        _map = map;
        _playersConnection = new ConcurrentDictionary<uint, IConnection>();
        _logger = logger;
    }

    /// <summary>
    ///     Gets all creatures in game
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ICreature> GetCreatures()
    {
        return _creatureInstances.All();
    }

    /// <summary>
    ///     Adds killed monsters
    /// </summary>
    /// <param name="monster"></param>
    public void AddKilledMonsters(IMonster monster)
    {
        _creatureInstances.TryRemove(monster.CreatureId);
        _creatureInstances.AddKilledMonsters(monster);
    }

    /// <summary>
    ///     Gets creature instance on game
    /// </summary>
    /// <param name="id">Creature Id</param>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool TryGetPlayer(uint id, out IPlayer player)
    {
        player = default;
        if (TryGetCreature(id, out var creature) && creature is IPlayer)
        {
            player = (IPlayer)creature;
            return true;
        }

        return false;
    }

    public virtual bool TryGetPlayer(string name, out IPlayer player)
    {
        player = default;

        if (string.IsNullOrWhiteSpace(name)) return false;

        var creature = _creatureInstances.All().FirstOrDefault(x =>
            x is IPlayer playerFound &&
            playerFound.Name.Trim().Equals(name.Trim(), StringComparison.InvariantCultureIgnoreCase));

        if (creature is not IPlayer p) return false;

        player = p;
        return true;
    }

    public bool IsPlayerLogged(IPlayer player)
    {
        return _creatureInstances.TryGetPlayer(player.Id, out player);
    }

    public bool TryGetLoggedPlayer(uint playerId, out IPlayer player)
    {
        return _creatureInstances.TryGetPlayer(playerId, out player);
    }

    public IEnumerable<IPlayer> GetAllLoggedPlayers()
    {
        return _creatureInstances.AllLoggedPlayers();
    }

    /// <summary>
    ///     Returns a creature by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="creature"></param>
    /// <returns></returns>
    public bool TryGetCreature(uint id, out ICreature creature)
    {
        return _creatureInstances.TryGetCreature(id, out creature);
    }

    /// <summary>
    ///     Removes creature from game
    ///     This method also removes creature from map
    /// </summary>
    /// <param name="creature"></param>
    /// <returns></returns>
    public bool RemoveCreature(ICreature creature)
    {
        if (creature is IWalkableCreature walkableCreature) _map.RemoveCreature(walkableCreature);

        _creatureInstances.TryRemove(creature.CreatureId);

        //todo remove summons
        return true;
    }

    /// <summary>
    ///     Adds player to game
    ///     This methods also adds player to map and to connection pool
    /// </summary>
    /// <param name="player"></param>
    /// <param name="connection"></param>
    /// <returns></returns>
    public IPlayer AddPlayer(IPlayer player, IConnection connection)
    {
        var playerIsLogged = _creatureInstances.TryGetPlayer(player.Id, out var playerLogged);
        player = playerLogged ?? player;

        connection.SetConnectionOwner(player);

        _playersConnection.AddOrUpdate(player.CreatureId, connection, (_, _) => connection);

        if (playerIsLogged) return player;

        AddCreature(player);
        _creatureInstances.AddPlayer(player);

        return player;
    }

    /// <summary>
    ///     Removes player from game, map and connection pool
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool RemovePlayer(IPlayer player)
    {
        if (_playersConnection.TryRemove(player.CreatureId, out var connection))
            connection.Disconnect();
        _creatureInstances.TryRemoveFromLoggedPlayers(player.Id);

        RemoveCreature(player);

        _logger.Information("{Player} was removed from game", player.Name);

        return true;
    }

    /// <summary>
    ///     Gets the player connection
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="connection"></param>
    /// <returns></returns>
    public virtual bool GetPlayerConnection(uint playerId, out IConnection connection)
    {
        return _playersConnection.TryGetValue(playerId, out connection);
    }

    /// <summary>
    ///     Adds creature to game and to map
    /// </summary>
    /// <param name="creature"></param>
    /// <returns></returns>
    public bool AddCreature(ICreature creature)
    {
        _creatureInstances.Add(creature);
        return true;
    }

    /// <summary>
    ///     Returns all killed monsters
    /// </summary>
    /// <returns></returns>
    public IImmutableList<Tuple<IMonster, TimeSpan>> GetKilledMonsters()
    {
        return _creatureInstances.AllKilledMonsters();
    }
}