using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Items.Types.Containers;

public interface IDepot : IContainer
{
    bool IsAlreadyOpened { get; }
    void SetAsOpened(IPlayer openedBy);
    bool CanBeOpenedBy(IPlayer player);
}