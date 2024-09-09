namespace Server.Entities.Models.Contracts.Items;

public interface IItemEventSubscriber
{
    public void Subscribe(IItem item);
    public void Unsubscribe(IItem item);
}