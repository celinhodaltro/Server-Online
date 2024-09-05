using Data.Interfaces;
using Game.Common.Contracts.Creatures;
using Game.Common.Helpers;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerLoggedInEventHandler : IEventHandler
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerLoggedInEventHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async void Execute(IWalkableCreature creature)
    {
        if (creature.IsNull()) return;

        if (creature is not IPlayer player) return;

        await _playerRepository.UpdatePlayerOnlineStatus(player.Id, true);
    }
}