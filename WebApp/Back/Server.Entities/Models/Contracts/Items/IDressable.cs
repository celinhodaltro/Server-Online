using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Items;

public interface IDressable
{
    void DressedIn(IPlayer player);
    void UndressFrom(IPlayer player);
}