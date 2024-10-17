using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Contracts.Contracts.Network;

namespace Server.Contracts.Contracts;

public interface IGameCreatureManager
{
    void AddKilledMonsters(IMonster monster);
    IPlayer AddPlayer(IPlayer player, IConnection connection);
    IEnumerable<IPlayer> GetAllLoggedPlayers();
    IEnumerable<ICreature> GetCreatures();
    bool GetPlayerConnection(uint playerId, out IConnection connection);
    bool IsPlayerLogged(IPlayer player);
    bool RemoveCreature(ICreature creature);
    bool RemovePlayer(IPlayer player);
    bool TryGetCreature(uint id, out ICreature creature);
    bool TryGetLoggedPlayer(uint playerId, out IPlayer player);
    bool TryGetPlayer(string name, out IPlayer player);
    bool TryGetPlayer(uint id, out IPlayer player);
}