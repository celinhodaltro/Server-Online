namespace Server.Entities.Common.Contracts.Chats;

public interface IChatChannelEventSubscriber
{
    public void Subscribe(IChatChannel chatChannel);
    public void Unsubscribe(IChatChannel chatChannel);
}