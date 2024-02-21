using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using Extensions = Microsoft.Maui.Controls.Xaml.Extensions;

namespace VideoDemos.Views;

public partial class LoadingPage : ContentPage
{
    private readonly AuthService _authService;

    public LoadingPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (await _authService.IsAuthenticatedAsync())
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(AuthPage)}");
        }
    }

    private void LoadingPage_OnLoaded(object sender, EventArgs e)
    {
        if (_authService.IsAuthenticated())
        {
            string apiUrl = "http://telurxs-001-site1.ftempurl.com/api/refresh";


            // Create the HttpWebRequest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            string payload = $"{{\n  \"refreshToken\":\"{_authService.GetRefreshToken()}\"}}";
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
                        _authService.RefreshData(responseBody);
                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode + " - " + response.StatusDescription);
                        _authService.Logout();
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                _authService.Logout();
            }
        }
    }
}