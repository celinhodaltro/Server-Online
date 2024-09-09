using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts;

public interface IFactory
{
    public event CreateItem OnItemCreated;
}