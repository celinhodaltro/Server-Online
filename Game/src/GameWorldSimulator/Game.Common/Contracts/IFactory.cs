using Game.Common.Contracts.Items;

namespace Game.Common.Contracts;

public interface IFactory
{
    public event CreateItem OnItemCreated;
}