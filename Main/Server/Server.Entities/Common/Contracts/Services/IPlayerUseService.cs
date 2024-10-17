using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Contracts.Items.Types.Usable;

namespace Server.Entities.Common.Contracts.Services;

public interface IPlayerUseService
{
    void Use(IPlayer player, IItem item);
    void Use(IPlayer player, IUsableOn usableItem, IThing usedOn);
    void Use(IPlayer player, IContainer container, byte openAtIndex);
}