using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Services;

public interface ISummonService
{
    IMonster Summon(IMonster master, string summonName);
}