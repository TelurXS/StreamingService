using Domain.Entities;
using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface IRateService
{
	Task<GetResult<Rate> > GetRateByTitleAsync(Title title); 
}
