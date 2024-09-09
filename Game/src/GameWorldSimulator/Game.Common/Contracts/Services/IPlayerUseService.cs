using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Items.Types.Containers;
using Game.Common.Contracts.Items.Types.Usable;

namespace Game.Common.Contracts.Services;

public interface IPlayerUseService
{
    void Use(IPlayer player, IItem item);
    void Use(IPlayer player, IUsableOn usableItem, IThing usedOn);
    void Use(IPlayer player, IContainer container, byte openAtIndex);
}