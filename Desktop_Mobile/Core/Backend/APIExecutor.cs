using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;
using Metflix.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoDemos.Core.Auth;

namespace VideoDemos.Core.Backend;

public class APIExecutor
{
    public static string ExecutePost(string link, string jsonData = "")
    {
        string result = "";
        AuthService service = new AuthService();

        // Create the HttpWebRequest
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
        request.Headers.Add("Authorization", "Bearer " + service.GetAccessToken());
        request.ContentType = "application/json";
        request.MediaType = "application/json";
        request.Accept = "application/json";
        request.Method = "POST";
        try
        {
            if (jsonData != "")
            {
                // Convert payload to byte array
                byte[] payloadBytes = Encoding.UTF8.GetBytes(jsonData);
                request.ContentLength = payloadBytes.Length;
                // Write payload to request stream
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(payloadBytes, 0, payloadBytes.Length);
                }
            }

            // Get the response
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = reader.ReadToEnd();
                }
                else
                {
                    result = "Error: " + response.StatusDescription;
                }
            }
        }
        catch (WebException ex)
        {
            result = "Exception: " + ex.Message;
        }


        return result;
    }

    public static string ExecuteGet(string link, string jsonData = "")
    {
        string result = "";
        AuthService service = new AuthService();

        // Create the HttpWebRequest
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
        request.Method = "GET";
        request.Headers.Add("Authorization", "Bearer " + service.GetAccessToken());

        try
        {
            if (jsonData != "")
            {
                // Convert payload to byte array
                byte[] payloadBytes = Encoding.UTF8.GetBytes(jsonData);
                request.ContentLength = payloadBytes.Length;
                // Write payload to request stream
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(payloadBytes, 0, payloadBytes.Length);
                }
            }

            // Get the response
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = reader.ReadToEnd();
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode + " - " + response.StatusDescription);
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }


        return result;
    }
}