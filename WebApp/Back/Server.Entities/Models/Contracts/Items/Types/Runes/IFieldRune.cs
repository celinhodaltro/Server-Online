using Server.Entities.Models.Contracts.Items.Types.Usable;

namespace Server.Entities.Models.Contracts.Items.Types.Runes;

public interface IFieldRune : IUsableOnTile, IRune
{
    string Area { get; }
    ushort Field { get; }
}