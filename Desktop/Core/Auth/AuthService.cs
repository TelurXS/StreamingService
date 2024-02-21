using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace VideoDemos.Core.Auth;

public class AuthService
{
    private const string AuthStateKey = "AuthState";
    private const string AccessToken = "AccessToken";
    private const string RefreshToken = "RefreshToken";

    public async Task<bool> IsAuthenticatedAsync()
    {
        await Task.Delay(2000);
        var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
        return authState;
    }

    public string GetRefreshToken()
    {
        return Preferences.Default.Get<string>(RefreshToken, "null");
    }

    public string GetAccessToken()
    {
        return Preferences.Default.Get<string>(AccessToken, "null");
    }

    public bool IsAuthenticated()
    {
        var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
        return authState;
    }

    public void RefreshData(string jData)
    {
        var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(jData);


        Preferences.Default.Set<bool>(AuthStateKey, true);
        Preferences.Default.Set<string>(AccessToken, tokenInfo.AccessToken);
        Preferences.Default.Set<string>(RefreshToken, tokenInfo.RefreshToken);
    }
    public string Login(string login, string password)
    {
        try
        {
            string result = "";

            result = SendPostRequest(login, password);

            if (result.IsNullOrEmpty()) return "Incorrect login or password";
            var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(result);


            Preferences.Default.Set<bool>(AuthStateKey, true);
            Preferences.Default.Set<string>(AccessToken, tokenInfo.AccessToken);
            Preferences.Default.Set<string>(RefreshToken, tokenInfo.RefreshToken);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return "";
    }

    public void Logout()
    {
        // Preferences.Default.Set<string>(AccessToken, "");
        // Preferences.Default.Set<string>(RefreshToken,"");
        Preferences.Default.Clear();
        // Preferences.Default.Remove(AuthStateKey);
    }

    static string SendPostRequest(string login, string password)
    {
        // API endpoint URL
        string apiUrl = "http://telurxs-001-site1.ftempurl.com/api/login";

        // Request payload

        string payload = JsonConvert.SerializeObject(new LoginUserCredentials()
        {
            Email = login,
            Password = password,
            TwoFactorCode = "string",
            TwoFactorRecoveryCode = "strings"
        });

        // Create the HttpWebRequest
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        // Convert payload to byte array
        byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
        request.ContentLength = payloadBytes.Length;

        try
        {
            // Write payload to request stream
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(payloadBytes, 0, payloadBytes.Length);
            }

            // Get the response
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = reader.ReadToEnd();
                    return responseBody;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode + " - " + response.StatusDescription);
                    return "";
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }

        return "";
    }
}

public class TokenInfo
{
    [JsonProperty("accessToken")] public string AccessToken { get; set; }

    [JsonProperty("refreshToken")] public string RefreshToken { get; set; }
}