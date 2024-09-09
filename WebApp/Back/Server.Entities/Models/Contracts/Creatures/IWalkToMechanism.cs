using Server.Entities.Models.Location.Structs;
using System;

namespace Server.Entities.Models.Contracts.Creatures;

public interface IWalkToMechanism
{
    void WalkTo(IPlayer player, Action action, Location.Structs.Location toLocation, bool secondChance = false);
}