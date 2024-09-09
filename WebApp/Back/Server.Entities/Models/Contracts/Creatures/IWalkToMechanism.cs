using Game.Common.Location.Structs;
using System;

namespace Server.Entities.Models.Contracts.Creatures;

public interface IWalkToMechanism
{
    void WalkTo(IPlayer player, Action action, Location toLocation, bool secondChance = false);
}