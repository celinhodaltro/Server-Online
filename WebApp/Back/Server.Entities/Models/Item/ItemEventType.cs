namespace Server.Entities.Models.Item;

public enum ItemEventType : byte
{
    Use,
    MultiUse,
    Movement,
    Collision,
    Separation
}