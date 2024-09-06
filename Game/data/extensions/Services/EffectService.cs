using Game.Common.Contracts.World;
using Game.Common.Creatures;
using Game.Common.Location.Structs;
using Networking.Packets.Outgoing.Effect;
using Server.Common.Contracts;
using Server.Helpers;

namespace Extensions.Services;

public static class EffectService
{
    public static void Send(Location location, EffectT effect)
    {
        var map = IoC.GetInstance<IMap>();
        var game = IoC.GetInstance<IGameServer>();

        foreach (var spectator in map.GetPlayersAtPositionZone(location))
        {
            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            connection.OutgoingPackets.Enqueue(new MagicEffectPacket(location, effect));
            connection.Send();
        }
    }
}