using Flipped.Utilities;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Flipped.Frames.Windows
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        private const string Version = "0.1.3";
        private bool valid = true;

        public LoadingScreen()
        {
            InitializeComponent();
            InitializeLauncher();
        }

        private async void InitializeLauncher()
        {
            await FetchVersion($"http://{LauncherIps.Backend}/fetch/version");
            if (valid)
            {
                var backend = new Backend($"http://{LauncherIps.Backend}");
                string discordId = SavedData.ReadValue("Auth", "DiscordId");
                string hwid = SavedData.ReadValue("Auth", "hwid");

                if (string.IsNullOrEmpty(discordId) || string.IsNullOrEmpty(hwid))
                {
                    var login = new LoginScreen();
                    login.Show();
                    this.Close();
                    return;
                }

                string response = await backend.GetLauncherUsername(discordId);
                string discordCallbackId = await backend.HWIDBanCheck(hwid);
                string savedDiscordId = SavedData.ReadValue("Auth", "DiscordId");

                if (discordCallbackId.Trim('"') != savedDiscordId || response == "false")
                {
                    var login = new LoginScreen();
                    login.Show();
                    this.Close();
                    SavedData.RemoveKey("Auth", "DiscordId");
                    SavedData.RemoveKey("Auth", "hwid");
                }
                else
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
        }

        private async Task FetchVersion(string endpoint)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(new Uri(endpoint));
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                    if (data.version != Version)
                    {
                        MessageBox.Show("Your launcher is out of date, the launcher will now shut down", "Launcher");
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not check for latest version, launcher will now shut down", "Launcher");
                Application.Current.Shutdown();
            }
        }
    }
}
