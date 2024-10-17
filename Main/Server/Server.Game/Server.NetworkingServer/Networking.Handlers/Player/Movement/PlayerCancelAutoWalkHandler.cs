using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player.Movement;

public class PlayerCancelAutoWalkHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerCancelAutoWalkHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player))
            _game.Dispatcher.AddEvent(new Event(player.StopWalking));
    }
}