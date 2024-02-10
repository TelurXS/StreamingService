using System.Diagnostics;

namespace VideoDemos.Core.Auth;

public class AuthService
{
    private const string AuthStateKey = "AuthState";
    private const string UserIdKey = "UserId";
    public async Task<bool> IsAuthenticatedAsync()
    {
        await Task.Delay(2000);
        var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
        return authState;
    }   
    public bool IsAuthenticated()
    {
        var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
        return authState;
    }


    public int GetUserId()
    {
        return Preferences.Default.Get<int>(AuthStateKey, -1);
    }
    public void Login(int userId)
    {
        Preferences.Default.Set<bool>(AuthStateKey, true);
        Preferences.Default.Set<int>(UserIdKey, userId);
    }
    public void Logout() 
    {
        Preferences.Default.Remove(AuthStateKey);
        Preferences.Default.Set<int>(UserIdKey, -1);
    }
}