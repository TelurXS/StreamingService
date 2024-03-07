namespace Metflix.Core.Models;

public class Banner
{
    private int _id;
    private string _image;
    private string _name;
    private string _video_link;
    private string _description;
    private float _rating;
    private int _realise_year;
    private int _watchedPrecent;

    public Banner(int id, string image, string name, string videoLink, string description)
    {
        _id = id;
        _image = image;
        _name = name;
        _video_link = videoLink;
        _description = description;
        _rating = 2.5f;
        _realise_year = 2000;
        _watchedPrecent = 15;
    }
    public Banner(int id, string image, string name, string videoLink, string description, float rating, int realiseYear)
    {
        _id = id;
        _image = image;
        _name = name;
        _video_link = videoLink;
        _description = description;
        _rating = rating;
        _realise_year = realiseYear;
        _watchedPrecent = 15;
    }

    public Banner(int id, string image, string name, string videoLink, string description, float rating, int realiseYear, int watchedPrecent)
    {
        _id = id;
        _image = image;
        _name = name;
        _video_link = videoLink;
        _description = description;
        _rating = rating;
        _realise_year = realiseYear;
        _watchedPrecent = watchedPrecent;
    }

    public int WatchedPrecent
    {
        get => _watchedPrecent;
        set => _watchedPrecent = value;
    }

    public float Rating
    {
        get => _rating;
        set => _rating = value;
    }

    public int RealiseYear
    {
        get => _realise_year;
        set => _realise_year = value;
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Image
    {
        get => _image;
        set => _image = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string VideoLink
    {
        get => _video_link;
        set => _video_link = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }
}