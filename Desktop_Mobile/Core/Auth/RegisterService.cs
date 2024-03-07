using System.Text;
using Metflix.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Auth.Registration;

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
        using (HttpClient client = new HttpClient())
        {
            string jsonData = JsonConvert.SerializeObject(new UserCredentials()
            {
                Email = AccountModel.Email,
                Password = AccountModel.Password,
            });

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(errorMessage);
                JObject errors = data["errors"].ToObject<JObject>();
                List<string> errorList = new List<string>();
                foreach (var error in errors)
                {
                    JArray errorMessages = error.Value as JArray;


                    foreach (var mes in errorMessages)
                    {
                        errorList.Add(mes.ToString());
                    }
                }

                _errorsList = errorList;
            }
        }
        string detailsJsonData = JsonConvert.SerializeObject(new UserDetails()
        {
            Name = AccountModel.Nickname,
            FirstName = AccountModel.Name,
            SecondName = AccountModel.Surname,
            BirthDate = AccountModel.Birthdate
        });
        APIExecutor.ExecutePost(Config.API_LINK + "/manage/profile", detailsJsonData);
        APIExecutor.ExecutePost(Config.API_LINK + "/manage/genres", JsonConvert.SerializeObject(AccountModel.Genres));
    }

    public static void EndPaymentRegister()
    {
        APIExecutor.ExecutePost(Config.API_LINK + "/manage/genres", JsonConvert.SerializeObject(AccountModel.Genres));
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