namespace Metflix.Core.Models;

public class VozModel
{
    private string _name;
    private bool _isEnabled;


    public VozModel(string name, bool isEnabled)
    {
        _name = name;
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
}