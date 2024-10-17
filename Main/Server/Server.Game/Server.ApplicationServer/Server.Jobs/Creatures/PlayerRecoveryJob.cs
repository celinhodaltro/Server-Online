using Server.Entities.Common.Contracts.Creatures;

namespace Server.Jobs.Creatures;

public static class PlayerRecoveryJob
{
    public static void Execute(IPlayer player)
    {
        if (player.IsDead) return;
        if (!player.Recovering) return;

        player.Recover();
    }
}