using System;
using Game.World.Models.Spawns;

namespace Server.Jobs.Creatures;

public static class RespawnJob
{
    private const int INTERVAL = 10000;
    private static long _lastRespawn;

    public static void Execute(SpawnManager spawnManager)
    {
        var now = DateTime.Now.Ticks;
        var remainingTime = TimeSpan.FromTicks(now - _lastRespawn).TotalMilliseconds;

        if (!(remainingTime >= INTERVAL)) return;

        spawnManager.Respawn();
        _lastRespawn = now;
    }
}