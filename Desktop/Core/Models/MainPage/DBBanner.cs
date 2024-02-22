namespace Metflix.Core.Models;

public class Title
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public float AvarageRate { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Type { get; set; }
    public int Country { get; set; }
    public int AgeRestriction { get; set; }
    public int Views { get; set; }
    public string Trailer { get; set; }
    public string Director { get; set; }
    public string Cast { get; set; }

    public List<DB_BannerName> Names { get; set; }
    public List<DB_BannerDescription> Descriptions { get; set; }
    public List<DB_Image> Screenshots { get; set; }
    public List<DB_Genre> Genres { get; set; }
    public List<DB_Series> Series { get; set; }
    public List<DB_Comment> Comments { get; set; }
    public DB_Image Image { get; set; }
}

public class DB_Series
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Uri { get; set; }
    public string Dubbing { get; set; }
    public string Language { get; set; }
    public int index { get; set; }
}

public class DB_Comment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public DateTime ReleaseDate { get; set; }

    public DBProfileModel Author { get; set; }
}

public class DB_Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class DB_Image
{
    public Guid Id { get; set; }
    public string Uri { get; set; }
}

public class DB_BannerName
{
    public Guid Id { get; set; }
    public string Language { get; set; }
    public string Value { get; set; }
}

public class DB_BannerDescription
{
    public Guid Id { get; set; }
    public string Language { get; set; }
    public string Value { get; set; }
}

public class DB_BannerImage
{
    public Guid Id { get; set; }
    public string Uri { get; set; }
}

public class DBBanner
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Availability { get; set; }
    public List<Title> Titles { get; set; }
}