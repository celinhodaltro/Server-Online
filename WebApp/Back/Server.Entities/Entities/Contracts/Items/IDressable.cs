using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.Items;

public interface IDressable
{
    void DressedIn(IPlayer player);
    void UndressFrom(IPlayer player);
}