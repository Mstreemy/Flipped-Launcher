using Flipped.Frames.Windows;
using Flipped.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flipped.Frames.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        string Version = "0.1.1";
        public Settings()
        {
            InitializeComponent();
            FetchInfo();
        }
        private void AccountsClick(object sender, RoutedEventArgs e)
        {
            BuildsFrame.Visibility = Visibility.Collapsed;
            BuildsBTN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#040d1c"));
            AccountsBTN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            AccountFrame.Visibility = Visibility.Visible;
            var fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.3),
            };
            AccountFrame.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }
        private void BuildsClick(object sender, RoutedEventArgs e)
        {
            AccountFrame.Visibility = Visibility.Collapsed;
            AccountsBTN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#040d1c"));
            BuildsBTN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            BuildsFrame.Visibility = Visibility.Visible;
            var fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.3),
            };
            BuildsFrame.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }
        private async void FetchInfo()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                var Backend = new Backend($"http://{LauncherIps.Backend}");
                string discordId = SavedData.ReadValue("Auth", "DiscordId");
                string response = await Backend.GetLauncherUsername(discordId);
                UsernameText.Text = response;
                string response2 = await Backend.GetLauncherAvatar(discordId);
                string localImagePath = $"{appDirectory}/Assets/UserAvatar.png";
                await DownloadImage(response2, localImagePath);
                PFP.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/UserAvatar.png", UriKind.Relative));
                DiscordId.Text = discordId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async Task DownloadImage(string imageUrl, string localPath)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageData = await client.GetByteArrayAsync(imageUrl);

                    System.IO.File.WriteAllBytes(localPath, imageData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error downloading image: {ex.Message}");
                }
            }
        }
        private async void UpdatesClick(object sender, RoutedEventArgs e)
        {
            await FetchVersion($"http://{LauncherIps.Backend}/fetch/version");
        }
        private void ResetClick(object sender, RoutedEventArgs e)
        {
            SavedData.RemoveKey("Auth", "Build1Path");
            SavedData.RemoveKey("Auth", "Build1Season");
            SavedData.RemoveKey("Auth", "Build2Path");
            SavedData.RemoveKey("Auth", "Build2Season");
            SavedData.RemoveKey("Auth", "Build3Path");
            SavedData.RemoveKey("Auth", "Build3Season");
            SavedData.RemoveKey("Auth", "Builds");
        }
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            SavedData.RemoveKey("Auth", "DiscordId");
            SavedData.RemoveKey("Auth", "hwid");
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            var login = new LoginScreen();
            login.Show();
            Window.GetWindow(this).Close();
        }
        private async Task FetchVersion(string endpoint)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(new Uri(endpoint));
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic Data = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    if (Data.version != Version)
                    {
                        MessageBox.Show("Your launcher is out of date, the launcher will now shut down", "Launcher");
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        MessageBox.Show("You are running the latest version of Flipped!", "Launcher");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not check for latest version, launcher will not shut down", "Launcher");
                Application.Current.Shutdown();
            }
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            this.IsEnabled = false;
            MainWindow mainWindow = new MainWindow();
            mainWindow.LeaveSettings();
            Window.GetWindow(this).Close();

        }
    }
}
