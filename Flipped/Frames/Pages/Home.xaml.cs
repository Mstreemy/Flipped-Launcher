using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Flipped.Utilities;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Flipped.Frames.Windows;
using Newtonsoft;
using System.IO;
using System.Net.Http;
using System.Diagnostics;

namespace Flipped.Frames.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private string NewsEndpoint1 = $"http://{LauncherIps.Backend}/fetch/news1";
        private string NewsEndpoint2 = $"http://{LauncherIps.Backend}/fetch/news2";
        private string NewsEndpoint3 = $"http://{LauncherIps.Backend}/fetch/news3";
        private string Version = "0.1.2";
        public Home()
        {
            InitializeComponent();
            Season1.Visibility = Visibility.Collapsed;
            Season2.Visibility = Visibility.Collapsed;
            Season3.Visibility = Visibility.Collapsed;
            CheckBuilds();
            FetchNews1();
            FetchInfo();
        }
        private void Season1Click(object sender, RoutedEventArgs e)
        {
            Library library = new Library();
            Thread launcherThread = new Thread(() => library.Launch("1"));
            launcherThread.Start();
            if (library.running == true)
            {
                Library.Kill(); 
            }
        }
        private void Season2Click(object sender, RoutedEventArgs e)
        {
            Library library = new Library();
            Thread launcherThread = new Thread(() => library.Launch("2"));
            launcherThread.Start();
            if (library.running == true)
            {
                Library.Kill();
            }
        }
        private void Season3Click(object sender, RoutedEventArgs e)
        {
            Library library = new Library();
            Thread launcherThread = new Thread(() => library.Launch("3"));
            launcherThread.Start();
            if (library.running == true)
            {
                Library.Kill();
            }
        }
        private void CheckBuilds()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (SavedData.ReadValue("Auth", "Build1Path") != null)
            {
                Season1.Visibility = Visibility.Visible;
                if (SavedData.ReadValue("Auth", "Build1Season") != null)
                {
                    string seasonNumber = SavedData.ReadValue("Auth", "Build1Season");
                    string numericPart = new string(seasonNumber.Where(char.IsDigit).ToArray());
                    string formattedSeasonTitle = $"Season {numericPart}";
                    Season1Title.Text = formattedSeasonTitle;
                }
                else
                {
                    Season1Title.Text = "Fortnite Build 1";
                }
                if (SavedData.ReadValue("Auth", "Build1Season") != null)
                {
                    Season1BG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/{SavedData.ReadValue("Auth", "Build1Season")}Poster.jpg", UriKind.Relative));
                }
                else
                {
                    Season1BG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/OtherBuild.jpg", UriKind.Relative));
                }
            }
            if (SavedData.ReadValue("Auth", "Build2Path") != null)
            {
                Season2.Visibility = Visibility.Visible;
                if (SavedData.ReadValue("Auth", "Build2Season") != null)
                {
                    string seasonNumber = SavedData.ReadValue("Auth", "Build2Season");
                    string numericPart = new string(seasonNumber.Where(char.IsDigit).ToArray());
                    string formattedSeasonTitle = $"Season {numericPart}";
                    Season2Title.Text = formattedSeasonTitle;
                }
                else
                {
                    Season2Title.Text = "Fortnite Build 2";
                }
                if (SavedData.ReadValue("Auth", "Build2Season") != null)
                {
                    Season2BG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/{SavedData.ReadValue("Auth", "Build2Season")}Poster.jpg", UriKind.Relative));
                }
                else
                {
                    Season2BG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/OtherBuild.jpg", UriKind.Relative));
                }
            }
            if (SavedData.ReadValue("Auth", "Build3Path") != null)
            {
                Season3.Visibility = Visibility.Visible;
                if (SavedData.ReadValue("Auth", "Build3Season") != null)
                {
                    string seasonNumber = SavedData.ReadValue("Auth", "Build3Season");
                    string numericPart = new string(seasonNumber.Where(char.IsDigit).ToArray());
                    string formattedSeasonTitle = $"Season {numericPart}";
                    Season3Title.Text = formattedSeasonTitle;
                }
                else
                {
                    Season3Title.Text = "Fortnite Build 3";
                }
                if (SavedData.ReadValue("Auth", "Build3Season") != null)
                {
                    Season3BG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/{SavedData.ReadValue("Auth", "Build3Season")}Poster.jpg", UriKind.Relative));
                }
                else
                {
                    Season3BG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/OtherBuild.jpg", UriKind.Relative));
                }
            }
        }
        private async Task FetchNews(string endpoint, TextBlock header, TextBlock date, TextBlock desc)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(new Uri(endpoint));

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        dynamic newsData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                        string newsHeader = newsData.header;
                        string newsDate = newsData.date;
                        string newsDesc = newsData.desc;

                        header.Text = newsHeader;
                        date.Text = newsDate;
                        desc.Text = newsDesc;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        private async void FetchNews1()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            NewsP1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            NewsP2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363636"));
            NewsP3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363636"));
            NewsBG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/NewsBG1.jpg", UriKind.Relative));
            await FetchNews(NewsEndpoint1, Header, Title, Content);
            await Task.Delay(5000);
            NewsP1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363636"));
            NewsP2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            NewsP3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363636"));
            NewsBG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/NewsBG2.jpg", UriKind.Relative));
            await FetchNews(NewsEndpoint2, Header, Title, Content);
            await Task.Delay(5000);
            NewsP1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363636"));
            NewsP2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363636"));
            NewsP3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            NewsBG.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/NewsBG3.jpg", UriKind.Relative));
            await FetchNews(NewsEndpoint3, Header, Title, Content);
            await Task.Delay(5000);
            FetchNews1();
        }
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            SavedData.RemoveKey("Auth", "DiscordId");
            SavedData.RemoveKey("Auth", "hwid");
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(appPath);
            Environment.Exit(0);
        }
        private async void FetchInfo()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                var Backend = new Backend($"http://{LauncherIps.Backend}");
                string discordId = SavedData.ReadValue("Auth", "DiscordId");
                string response = await Backend.GetLauncherUsername(discordId);
                UsernameText2.Text = response;
                UsernameText.Text = $"Welcome, {response}";
                string response2 = await Backend.GetLauncherAvatar(discordId);
                string localImagePath = $"{appDirectory}/Assets/UserAvatar.png";
                await DownloadImage(response2, localImagePath);
                PFP.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/UserAvatar.png", UriKind.Relative));
                UserID.Text = discordId;
                string response3 = await Backend.GetLauncherSkin(discordId);
                string skinUrl = response3;
                string skinPath = $"{appDirectory}/Assets/UserSkin.png";
                await DownloadImage(skinUrl, skinPath);
                UserSkin.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/UserSkin.png", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LogError(string errorMessage)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine($"[{DateTime.Now}] {errorMessage}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to log the error to file.");
            }
        }

        private string logFilePath = "error.log";
        public async Task DownloadImage(string imageUrl, string localPath)
        {
            Console.WriteLine($"Downloading image from URL: {imageUrl}");

            imageUrl = imageUrl.Trim('\"');

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Uri uri = new Uri(imageUrl);

                    HttpResponseMessage response = await client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] imageData = await response.Content.ReadAsByteArrayAsync();
                        File.WriteAllBytes(localPath, imageData);
                    }
                    else
                    {
                        LogError($"Failed to download image from URL: {imageUrl}\nHTTP Status Code: {response.StatusCode}");
                    }
                }
                catch (UriFormatException ex)
                {
                    LogError($"Invalid URL format: {imageUrl}\nError message: {ex.Message}");
                }
                catch (Exception ex)
                {
                    LogError($"Error downloading image from URL: {imageUrl}\nError message: {ex.Message}");
                }
            }
        }



    }
}
