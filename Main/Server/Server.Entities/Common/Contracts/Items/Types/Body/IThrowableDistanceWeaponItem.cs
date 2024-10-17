namespace Server.Entities.Common.Contracts.Items.Types.Body;

public interface IThrowableDistanceWeaponItem : ICumulative, IWeapon
{
    byte AttackPower { get; }
    byte Range { get; }
}