using Newtonsoft.Json;

namespace VideoDemos.Core.Models.Bookmarks;

public class DBBookmarkModel
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("availability")]
    public int Availability { get; set; }
}