using Metflix.Core.Models;

namespace Xflick.Core.Models.Notifications;

public class DB_Notification
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string LocalizabledMessage { get; set; }
    public string Link { get; set; }
    public bool Snoozed { get; set; }
    public DateTime Date { get; set; }
    public DBProfileModel RelatedUser { get; set; }
}