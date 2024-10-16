using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;
using Networking.Packets.Incoming;
using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player;

public class PlayerLookAtHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerLookAtHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var lookAtPacket = new LookAtPacket(message);

        if (_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player))
        {
            if (lookAtPacket.Location.Type == LocationType.Ground)
            {
                if (_game.Map[lookAtPacket.Location] is not ITile tile) return;

                _game.Dispatcher.AddEvent(new Event(() => player.LookAt(tile)));
            }

            if (lookAtPacket.Location.Type == LocationType.Container)
                _game.Dispatcher.AddEvent(new Event(() =>
                    player.LookAt(lookAtPacket.Location.ContainerId, lookAtPacket.Location.ContainerSlot)));

            if (lookAtPacket.Location.Type == LocationType.Slot)
                _game.Dispatcher.AddEvent(new Event(() => player.LookAt(lookAtPacket.Location.Slot)));
        }
    }
}