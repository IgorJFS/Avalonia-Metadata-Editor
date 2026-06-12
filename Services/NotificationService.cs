using System;
using Avalonia.Controls.Notifications;

namespace Docs_Metadata_Editor.Services;

/// <summary>
/// Service implementation that wraps Avalonia's native WindowNotificationManager to show toast notifications.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly WindowNotificationManager _notificationManager;

    /// <summary>
    /// Initializes a new instance of the NotificationService class.
    /// </summary>
    /// <param name="notificationManager">The window notification manager overlay from the view.</param>
    public NotificationService(WindowNotificationManager notificationManager)
    {
        _notificationManager = notificationManager;
    }

    public void ShowSuccess(string title, string message)
    {
        _notificationManager.Show(new Notification(
            title,
            message,
            NotificationType.Success,
            TimeSpan.FromSeconds(4)));
    }

    public void ShowInfo(string title, string message)
    {
        _notificationManager.Show(new Notification(
            title,
            message,
            NotificationType.Information,
            TimeSpan.FromSeconds(4)));
    }

    public void ShowError(string title, string message)
    {
        _notificationManager.Show(new Notification(
            title,
            message,
            NotificationType.Error,
            TimeSpan.FromSeconds(5)));
    }
}
