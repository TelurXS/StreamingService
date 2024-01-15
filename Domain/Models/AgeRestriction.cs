using System.ComponentModel;

namespace Domain.Models;

public enum AgeRestriction
{
    [Description("All ages admitted")] GeneralAudiences,
    [Description("Some material may not be suitable for children")] ParentalGuidanceSuggested,
    [Description("Some material may be inappropriate for children under 13")] ParentsStronglyCautioned,
    [Description("Under 17 requires accompanying parent or adult guardian")] Restricted,
    [Description("No one 17 and under admitted")] AdultsOnly,
}
