using System.ComponentModel;

namespace Domain.Models;

public enum TitleSorting
{
	[Description("None")] None,
	[Description("New")] ByNewness,
	[Description("Old")] ByOldness,
	[Description("Rating")] ByRating,
	[Description("Popularity")] ByPopularity
}
