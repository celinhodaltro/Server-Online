using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Services;

public interface ISummonService
{
    IMonster Summon(IMonster master, string summonName);
}