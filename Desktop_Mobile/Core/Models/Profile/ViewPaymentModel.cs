namespace Metflix.Core.Models;

public class ViewPaymentModel
{
    private string _icon;
    private string _details;

    public string Icon
    {
        get => _icon;
        set => _icon = value;
    }

    public string Details
    {
        get => _details;
        set => _details = value;
    }

    public ViewPaymentModel(string icon, string details)
    {
        _icon = icon;
        _details = details;
    }
}