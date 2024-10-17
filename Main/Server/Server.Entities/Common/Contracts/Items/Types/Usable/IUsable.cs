using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Items.Types.Usable;

public interface IUsable
{
    /// <summary>
    ///     Method to use the item by the player.
    /// </summary>
    /// <param name="usedBy">The player who is using the item.</param>
    void Use(IPlayer usedBy);
}