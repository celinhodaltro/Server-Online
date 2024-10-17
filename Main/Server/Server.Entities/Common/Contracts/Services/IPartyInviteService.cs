using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Services;

public interface IPartyInviteService
{
    void Invite(IPlayer player, IPlayer invited);
}