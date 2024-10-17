using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Items.Types.Containers;

public interface IDepot : IContainer
{
    bool IsAlreadyOpened { get; }
    void SetAsOpened(IPlayer openedBy);
    bool CanBeOpenedBy(IPlayer player);
}