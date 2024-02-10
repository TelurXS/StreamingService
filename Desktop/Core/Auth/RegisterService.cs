using Metflix.Core.Models;

namespace VideoDemos.Core.Auth;

public class RegisterService
{
    public static AccountModel AccountModel;

    public static void StartRegister(string email, string password)
    {
        AccountModel = new AccountModel();
    }

    public static void EndProfileRegister()
    {
        
    }

    public static void EndPaymentRegister()
    {
        
    }
}