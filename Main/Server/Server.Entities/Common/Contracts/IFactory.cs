using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts;

public interface IFactory
{
    public event CreateItem OnItemCreated;
}