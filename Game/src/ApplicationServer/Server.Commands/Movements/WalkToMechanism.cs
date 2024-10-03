using System;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Location.Structs;
using Server.Common.Contracts;
using Server.Tasks;

namespace Server.Commands.Movements;

public class WalkToMechanism : IWalkToMechanism
{
    private readonly IGameServer game;

    public WalkToMechanism(IGameServer game)
    {
        this.game = game;
    }

    public void WalkTo(IPlayer player, Action action, Location toLocation, bool secondChance = false)
    {
        if (!toLocation.IsNextTo(player.Location))
        {
            if (secondChance) return;

            Action<ICreature> callBack = _ =>
                game.Scheduler.AddEvent(new SchedulerEvent(player.StepDelay,
                    () => WalkTo(player, action, toLocation, true)));

            player.WalkTo(toLocation, callBack);
            return;
        }

        action?.Invoke();
    }
}