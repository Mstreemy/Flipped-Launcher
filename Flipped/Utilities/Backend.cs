using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flipped.Utilities
{
    internal class Backend
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl;

        public Backend(string baseUrl)
        {
            this.baseUrl = baseUrl;
            this.httpClient = new HttpClient();
        }

public async Task<string> LoginToken(string token)
{
    try
    {
        string requestUrl = $"{baseUrl}/launcher/loginToken?token={token}";

        HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            if (response.StatusCode == HttpStatusCode.InternalServerError) 
            {
                throw new Exception("Invalid token");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
            return null;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
        return null;
    }
}
        public async Task<string> GetLauncherExchangeCode(string discordId, string hwid)
        {
            try
            {
                string requestUrl = $"{baseUrl}/fetch/exchange_code?discordId={discordId}&hwid={hwid}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetLauncherHWIDCheck(string discordId, string hwid)
        {
            try
            {
                string requestUrl = $"{baseUrl}/launcher/loginHWID?discordId={discordId}&hwid={hwid}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetLauncherVbucks(string discordId)
        {
            try
            {
                string requestUrl = $"{baseUrl}/profile/vbucks?discordId={discordId}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetLauncherDiscordId(string token)
        {
            try
            {
                string requestUrl = $"{baseUrl}/launcher/getDiscordId?token={token}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    string discordId = JsonConvert.DeserializeObject<string>(jsonResponse);
                    return discordId;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
    

    public async Task<string> GetLauncherUsername(string discordId)
        {
            try
            {
                string requestUrl = $"{baseUrl}/launcher/getUsername?discordId={discordId}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> HWIDBanCheck(string hwid)
        {
            try
            {
                string requestUrl = $"{baseUrl}/launcher/checkhwid?hwid={hwid}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetLauncherAvatar(string discordId)
        {
            try
            {
                string requestUrl = $"{baseUrl}/launcher/getAvatar?discordId={discordId}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetLauncherSkin(string discordId)
        {
            try
            {
                string requestUrl = $"{baseUrl}/selectedSkin?discordId={discordId}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetLauncherPassword(string discordId)
        {
            try
            {
                string requestUrl = $"{baseUrl}/launcher/getPassword?discordId={discordId}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        private string ParseIconUrl(dynamic responseData)
        {
            try
            {
                string iconUrl = responseData["iconUrl"];

                return iconUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse icon URL: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetFeatured1()
        {
            try
            {
                // Build the request URL with the email parameter
                string requestUrl = $"{baseUrl}/featured1";

                // Make the GET request
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and return the response content
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle error cases
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetFeatured2()
        {
            try
            {
                // Build the request URL with the email parameter
                string requestUrl = $"{baseUrl}/featured2";

                // Make the GET request
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and return the response content
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle error cases
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        public async Task<string> GetDaily1()
        {
            try
            {
                // Build the request URL with the email parameter
                string requestUrl = $"{baseUrl}/daily1";

                // Make the GET request
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and return the response content
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle error cases
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetDaily2()
        {
            try
            {
                string requestUrl = $"{baseUrl}/daily2";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetDaily3()
        {
            try
            {
                string requestUrl = $"{baseUrl}/daily3";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetDaily4()
        {
            try
            {
                string requestUrl = $"{baseUrl}/daily4";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetDaily5()
        {
            try
            {
                string requestUrl = $"{baseUrl}/daily5";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetDaily6()
        {
            try
            {
                string requestUrl = $"{baseUrl}/daily6";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> BuyFeatured(string number, string discordId)
        {
            try
            {
                string requestUrl = $"{baseUrl}/purchase/featured{number}?discordId={discordId}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        public async Task<string> BuyDaily(string number, string discordId)
        {
            try
            {
                string requestUrl = $"{baseUrl}/purchase/daily{number}?discordId={discordId}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

    }

}