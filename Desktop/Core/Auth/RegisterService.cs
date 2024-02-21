using System.Text;
using Metflix.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VideoDemos.Core.Auth;

public class RegisterService
{
    public static AccountModel AccountModel;
    public static List<string> _errorsList = new List<string>();

    public static void StartRegister(string email, string password)
    {
        AccountModel = new AccountModel();
        AccountModel.Email = email;
        AccountModel.Password = password;
    }

    public static async void EndProfileRegister()
    {
        string apiUrl = "http://telurxs-001-site1.ftempurl.com/api/register";
        _errorsList = null;
        // Create an instance of HttpClient
        using (HttpClient client = new HttpClient())
        {
            // Set headers
            // client.DefaultRequestHeaders.Add("accept", "*/*");
            // client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            // JSON data to be sent in the request body
            string jsonData = JsonConvert.SerializeObject(new UserCredentials()
            {
                Email = AccountModel.Email,
                Password = AccountModel.Password,
            });

            // Convert JSON string to HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Make POST request
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            // Check if the request was successful (status code 2xx)
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("POST request successful");
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(errorMessage);
                JObject errors = data["errors"].ToObject<JObject>();
                List<string> errorList = new List<string>();
                foreach (var error in errors)
                {
                    string errorName = error.Key;
                    JArray errorMessages = error.Value as JArray;

                    Console.WriteLine($"Error Name: {errorName}");

                    foreach (var mes in errorMessages)
                    {
                        Console.WriteLine($"  Error Message: {mes}");
                        errorList.Add(mes.ToString());
                    }
                }

                Console.WriteLine($"Error message: {errorMessage}");
                _errorsList = errorList;
            }
        }
    }

    public static void EndPaymentRegister()
    {
    }
}

public class UserCredentials
{
    [JsonProperty("email")] public string Email { get; set; }

    [JsonProperty("password")] public string Password { get; set; }
}

public class LoginUserCredentials : UserCredentials
{
    [JsonProperty("twoFactorCode")] public string TwoFactorCode { get; set; }

    [JsonProperty("twoFactorRecoveryCode")]
    public string TwoFactorRecoveryCode { get; set; }
}