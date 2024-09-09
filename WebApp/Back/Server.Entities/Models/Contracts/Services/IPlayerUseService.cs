using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Contracts.Items.Types.Containers;
using Server.Entities.Models.Contracts.Items.Types.Usable;

namespace Server.Entities.Models.Contracts.Services;

public interface IPlayerUseService
{
    void Use(IPlayer player, IItem item);
    void Use(IPlayer player, IUsableOn usableItem, IThing usedOn);
    void Use(IPlayer player, IContainer container, byte openAtIndex);
}