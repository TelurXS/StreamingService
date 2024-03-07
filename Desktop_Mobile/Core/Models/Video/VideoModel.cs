namespace Metflix.Core.Models;

public class VideoModel
{
    private int _id;
    private int? _episode;
    private string _name;
    private string _url;
    private TimeSpan _watched;
    private bool _isWatched;
    private bool _isFilm;
    private VideoModel? _nextEpisode;

    public VideoModel(string name, string url)
    {
        _name = name;
        _url = url;
    }

    public VideoModel(int? episode, string name, string url, bool isFilm, VideoModel nextEpisode)
    {
        _episode = episode;
        _name = name;
        _url = url;
        _isFilm = isFilm;
        _nextEpisode = nextEpisode;
    }

    public VideoModel(int id, int? episode, string name, string url, TimeSpan watched, bool isWatched, bool isFilm, VideoModel nextEpisode)
    {
        _id = id;
        _episode = episode;
        _name = name;
        _url = url;
        _watched = watched;
        _isWatched = isWatched;
        _isFilm = isFilm;
        _nextEpisode = nextEpisode;
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public int? Episode
    {
        get => _episode;
        set => _episode = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string Url
    {
        get => _url;
        set => _url = value;
    }

    public TimeSpan Watched
    {
        get => _watched;
        set => _watched = value;
    }

    public bool IsWatched
    {
        get => _isWatched;
        set => _isWatched = value;
    }

    public bool IsFilm
    {
        get => _isFilm;
        set => _isFilm = value;
    }

    public VideoModel NextEpisode
    {
        get => _nextEpisode;
        set => _nextEpisode = value;
    }
}   