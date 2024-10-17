using Networking.Packets.Incoming;
using Server.Commands.Player;
using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player;

public class PlayerThrowItemHandler : PacketHandler
{
    private readonly IGameServer _game;
    private readonly PlayerThrowItemCommand _playerThrowItemCommand;

    public PlayerThrowItemHandler(IGameServer game, PlayerThrowItemCommand playerThrowItemCommand)
    {
        _game = game;
        _playerThrowItemCommand = playerThrowItemCommand;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var itemThrowPacket = new ItemThrowPacket(message);
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        _game.Dispatcher.AddEvent(new Event(2000,
            () => _playerThrowItemCommand.Execute(player,
                itemThrowPacket))); //todo create a const for 2000 expiration time
    }
}