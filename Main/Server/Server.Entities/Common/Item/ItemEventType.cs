namespace Server.Entities.Common.Item;

public enum ItemEventType : byte
{
    Use,
    MultiUse,
    Movement,
    Collision,
    Separation
}