using Networking.Packets.Incoming;
using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player.Movement;

public class PlayerAutoWalkHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerAutoWalkHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var autoWalk = new AutoWalkPacket(message);

        if (_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player))
            _game.Dispatcher.AddEvent(new Event(() => player.WalkTo(autoWalk.Steps)));
    }
}