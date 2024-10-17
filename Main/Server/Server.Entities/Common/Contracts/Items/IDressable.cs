using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Items;

public interface IDressable
{
    void DressedIn(IPlayer player);
    void UndressFrom(IPlayer player);
}