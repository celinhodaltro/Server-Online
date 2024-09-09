using Server.Entities.Models.Contracts.Items.Types;

namespace Server.Entities.Models.Contracts.Items.Types.Body;

public interface IThrowableDistanceWeaponItem : ICumulative, IWeapon
{
    byte AttackPower { get; }
    byte Range { get; }
}