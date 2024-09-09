using Server.Entities.Models;
using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Services;

public static class NotificationSenderService
{
    public static event SendNotification OnNotificationSent;

    public static void Send(IPlayer to, string message,
        NotificationType notificationType = NotificationType.Description)
    {
        OnNotificationSent?.Invoke(to, message, notificationType);
    }
}

public delegate void SendNotification(IPlayer player, string message, NotificationType notificationType);