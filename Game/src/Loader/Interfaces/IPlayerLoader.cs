using Data.Entities;
using Game.Common.Contracts.Creatures;

namespace Loader.Interfaces;

public interface IPlayerLoader
{
    IPlayer Load(PlayerEntity playerEntity);
    bool IsApplicable(PlayerEntity player);
}