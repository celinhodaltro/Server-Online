using Data.Interfaces;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Player;
using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;

namespace Networking.Handlers.Player;

public class PlayerRequestOutFitHandler : PacketHandler
{
    private readonly IGameServer _game;
    private readonly IPlayerOutFitStore _playerOutFitStore;
    private readonly IPlayerRepository _playerRepository;

    public PlayerRequestOutFitHandler(IGameServer game, IPlayerOutFitStore playerOutFitStore,
        IPlayerRepository playerRepository)
    {
        _game = game;
        _playerOutFitStore = playerOutFitStore;
        _playerRepository = playerRepository;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        if (player.IsNull()) return;

        var outfits = _playerOutFitStore.Get(player.Gender);

        var playerAddons = _playerRepository.GetOutfitAddons((int)player.Id).Result;

        connection.OutgoingPackets.Enqueue(new PlayerOutFitWindowPacket(player, outfits, playerAddons));
        connection.Send();
    }
}