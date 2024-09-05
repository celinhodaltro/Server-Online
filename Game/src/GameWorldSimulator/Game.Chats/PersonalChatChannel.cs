using Game.Common.Contracts.Chats;
using Game.Common.Contracts.Creatures;

namespace Game.Chats;

public abstract class PersonalChatChannel : ChatChannel, IChatChannel
{
    public PersonalChatChannel(ushort id, string name) : base(id, name)
    {
    }

    public new abstract string Name { get; }

    public override bool AddUser(IPlayer player)
    {
        if (users.Count == 1) return false;
        return base.AddUser(player);
    }
}