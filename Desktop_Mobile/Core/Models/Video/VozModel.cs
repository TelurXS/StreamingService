namespace Metflix.Core.Models;

public class VozModel
{
    private string _name;
    private string _link;
    private bool _isEnabled;

    public VozModel(string name, string link)
    {
        _name = name;
        _link = link;
        _isEnabled = false;
    }

    public VozModel(string name, string link, bool isEnabled)
    {
        _name = name;
        _link = link;
        _isEnabled = isEnabled;
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => _isEnabled = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string Link
    {
        get => _link;
        set => _link = value;
    }
}