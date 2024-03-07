namespace Metflix.Core.Models;

public class SettingsDataModel
{
    private string _email;
    private string _card4LastDidgits;
    private string _planName;
    private string _nextPaymentDate;
    private string _subscribeFrom;

    public string SubscribeFrom
    {
        get => _subscribeFrom;
        set => _subscribeFrom = value;
    }

    public string Email
    {
        get => _email;
        set => _email = value;
    }

    public string Card4LastDidgits
    {
        get => _card4LastDidgits;
        set => _card4LastDidgits = value;
    }

    public string PlanName
    {
        get => _planName;
        set => _planName = value;
    }

    public string NextPaymentDate
    {
        get => _nextPaymentDate;
        set => _nextPaymentDate = value;
    }
}