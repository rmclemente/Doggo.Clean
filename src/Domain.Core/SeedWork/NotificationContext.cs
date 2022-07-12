namespace Domain.Core.SeedWork;

public class NotificationContext
{
    private List<Notification> _notifications;
    public IReadOnlyCollection<Notification> Notifications => _notifications;
    public bool HasNotification => _notifications.Any();

    public NotificationContext()
    {
        _notifications = new List<Notification>();
    }

    public void AddNotification(Notification notification) => _notifications.Add(notification);

    public void ClearNotifications() => _notifications = new List<Notification>();
}