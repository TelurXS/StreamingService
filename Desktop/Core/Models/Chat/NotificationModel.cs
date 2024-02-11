namespace Metflix.Core.Models;

public class NotificationModel
{
    private string _avatar_url;
    private string _notification_text;
    private string _buttonText;
    private string _notificationHeadline;
    private DateTime _date;

    public virtual string NotificationHeadline
    {
        get => _notificationHeadline;
        set => _notificationHeadline = value;
    }

    public virtual string AvatarUrl
    {
        get => _avatar_url;
        set => _avatar_url = value;
    }

    public virtual string NotificationText
    {
        get => _notification_text;
        set => _notification_text = value;
    }

    public virtual string ButtonText
    {
        get => _buttonText;
        set => _buttonText = value;
    }

    public virtual DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    public NotificationModel(string avatarUrl, string notificationText, string buttonText, DateTime date)
    {
        _avatar_url = avatarUrl;
        _notification_text = notificationText;
        _buttonText = buttonText;
        _date = date;
    }

    public NotificationModel(string avatarUrl, string notificationText, string buttonText, string notificationHeadline, DateTime date)
    {
        _avatar_url = avatarUrl;
        _notification_text = notificationText;
        _buttonText = buttonText;
        _notificationHeadline = notificationHeadline;
        _date = date;
    }
}