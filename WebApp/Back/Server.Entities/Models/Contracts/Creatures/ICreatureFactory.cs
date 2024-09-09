using Server.Entities.Models.Contracts.World;

namespace Server.Entities.Models.Contracts.Creatures;

public delegate void CreatureCreated(ICreature creature);

public interface ICreatureFactory
{
    IMonster CreateMonster(string name, ISpawnPoint spawn = null);
    INpc CreateNpc(string name, ISpawnPoint spawn = null);
    IPlayer CreatePlayer(IPlayer playerModel);
    IMonster CreateSummon(string name, IMonster master);
    event CreatureCreated OnCreatureCreated;
}