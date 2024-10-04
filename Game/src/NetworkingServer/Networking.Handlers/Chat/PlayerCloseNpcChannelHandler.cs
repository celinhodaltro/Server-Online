using Server.Entities.Common.Contracts.Creatures;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Chat;

public class PlayerCloseNpcChannelHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerCloseNpcChannelHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        foreach (var creature in _game.Map.GetCreaturesAtPositionZone(player.Location))
            if (creature is INpc npc)
                _game.Dispatcher.AddEvent(new Event(() => npc.StopTalkingToCustomer(player)));
    }
}