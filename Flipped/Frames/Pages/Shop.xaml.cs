using Flipped.Utilities;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flipped.Frames.Pages
{
    /// <summary>
    /// Interaction logic for Shop.xaml
    /// </summary>
    public partial class Shop : Page
    {
        public Shop()
        {
            InitializeComponent();
            FetchInfo();
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
                string response2 = await Backend.GetLauncherAvatar(discordId);
                string localImagePath = $"{appDirectory}/Assets/UserAvatar.png";
                await DownloadImage(response2, localImagePath);
                PFP.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/UserAvatar.png", UriKind.Relative));
                UserID.Text = discordId;
                string response3 = await Backend.GetLauncherVbucks(discordId);
                VbucksText.Text = response3;
                // Time to get the items!
                string featured1 = await Backend.GetFeatured1();
                string f1p = $"{appDirectory}/Assets/Featured1.png";
                string[] parts = featured1.Split(';');
                F1Title.Text = parts[1];
                F1Price.Text = parts[2];
                await DownloadImage(parts[0], f1p);
                F1Image.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/Featured1.png", UriKind.Relative));
                // Number 2
                string featured2 = await Backend.GetFeatured2();
                string f2p = $"{appDirectory}/Assets/Featured2.png";
                string[] parts2 = featured2.Split(';');
                F2Title.Text = parts2[1];
                F2Price.Text = parts2[2];
                await DownloadImage(parts2[0], f2p);
                F2Image.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/Featured2.png", UriKind.Relative));
                await LoadDaily(1);
                await LoadDaily(2);
                await LoadDaily(3);
                await LoadDaily(4);
                await LoadDaily(5);
                await LoadDaily(6);
                LoadingBG.Visibility = Visibility.Collapsed; 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private async Task LoadDaily(int index)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string dailyData = null;
                switch (index)
                {
                    case 1:
                        dailyData = await Backend.GetDaily1();
                        break;
                    case 2:
                        dailyData = await Backend.GetDaily2();
                        break;
                    case 3:
                        dailyData = await Backend.GetDaily3();
                        break;
                    case 4:
                        dailyData = await Backend.GetDaily4();
                        break;
                    case 5:
                        dailyData = await Backend.GetDaily5();
                        break;
                    case 6:
                        dailyData = await Backend.GetDaily6();
                        break;
                    default:
                        Console.WriteLine("Invalid index.");
                        return;
                }
                string[] parts = dailyData.Split(';');
                if (parts.Length < 3)
                {
                    Console.WriteLine($"Invalid daily data format: {dailyData}");
                    return;
                }
                string imageUrl = parts[0];
                string name = parts[1];
                string price = parts[2];
                string imagePath = $"{appDirectory}/Assets/Daily{index}.png";
                await DownloadImage(imageUrl, imagePath);

                // Update UI with the loaded data
                switch (index)
                {
                    case 1:
                        D1Image.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        D1Name.Text = name;
                        D1Price.Text = price;
                        break;
                    case 2:
                        D2Image.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        D2Name.Text = name;
                        D2Price.Text = price;
                        break;
                    case 3:
                        D3Image.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        D3Name.Text = name;
                        D3Price.Text = price;
                        break;
                    case 4:
                        D4Image.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        D4Name.Text = name;
                        D4Price.Text = price;
                        break;
                    case 5:
                        D5Image.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        D5Name.Text = name;
                        D5Price.Text = price;
                        break;
                    case 6:
                        D6Image.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        D6Name.Text = name;
                        D6Price.Text = price;
                        break;
                    default:
                        Console.WriteLine("Invalid index.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Daily {index}: {ex.Message}");
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

        private async void F1Click(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.BuyFeatured("1", SavedData.ReadValue("Auth", "DiscordId"));
            MessageBox.Show(response);
        }
        private async void F2Click(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.BuyFeatured("2", SavedData.ReadValue("Auth", "DiscordId"));
            MessageBox.Show(response);
        }
        private async void D1Click(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.BuyDaily("1", SavedData.ReadValue("Auth", "DiscordId"));
            MessageBox.Show(response);
        }

        private async void D2Click(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.BuyDaily("2", SavedData.ReadValue("Auth", "DiscordId"));
            MessageBox.Show(response);
        }

        private async void D3Click(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.BuyDaily("3", SavedData.ReadValue("Auth", "DiscordId"));
            MessageBox.Show(response);
        }

        private async void D4Click(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.BuyDaily("4", SavedData.ReadValue("Auth", "DiscordId"));
            MessageBox.Show(response);
        }

        private async void D5Click(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.BuyDaily("5", SavedData.ReadValue("Auth", "DiscordId"));
            MessageBox.Show(response);
        }

        private async void D6Click(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.BuyDaily("6", SavedData.ReadValue("Auth", "DiscordId"));
            MessageBox.Show(response);
        }
    }
}
