using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Services;

public interface IPartyInviteService
{
    void Invite(IPlayer player, IPlayer invited);
}