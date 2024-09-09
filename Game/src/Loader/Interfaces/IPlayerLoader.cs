using Data.Entities;
using Server.Entities.Contracts.Creatures;

namespace Loader.Interfaces;

public interface IPlayerLoader
{
    IPlayer Load(PlayerEntity playerEntity);
    bool IsApplicable(PlayerEntity player);
}