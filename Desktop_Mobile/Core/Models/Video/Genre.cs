namespace Metflix.Core.Models;

public class Genre
{
    private int _id;
    private string _name;

    public Genre(string name)
    {
        _name = name;
        _id = 0;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }
}