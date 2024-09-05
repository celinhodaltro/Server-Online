using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.Services;

public interface ISummonService
{
    IMonster Summon(IMonster master, string summonName);
}