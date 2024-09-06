using Game.Common.Chats;
using Game.Common.Contracts.Creatures;
using Game.Common.Location.Structs;

namespace Extensions.Npcs;

public class NpcActionHandler
{
    public static void OnAnswer(INpc to, ICreature from, IDialog dialog, string message, SpeechType type)
    {
        switch (dialog.Action)
        {
            case "teleport":
                Teleport(from);
                break;
        }
    }

    private static void Teleport(ICreature creature)
    {
        if (creature is IPlayer player)
            player.TeleportTo((player.Location + new Location(1, 0, 0)).Location);
    }
}