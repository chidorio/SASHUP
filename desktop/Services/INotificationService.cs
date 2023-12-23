using System;
using Avalonia.Controls.Notifications;

namespace desktop;

public interface INotificationService
{
    public void RegisterNotificationManager(INotificationManager notificationManager);
    public void ShowNotification(INotification notification);
}
