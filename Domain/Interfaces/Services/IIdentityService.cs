using Domain.Models.Requests;

namespace Domain.Interfaces.Services;

public interface IIdentityService
{
	Task<string> RegisterAsync(RegisterRequest request);

	Task<bool> ConfirmEmailAsync(string userId, string code);
}
