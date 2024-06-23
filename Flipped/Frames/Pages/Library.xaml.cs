using Flipped.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SourceChord.FluentWPF;
using MicaWPF.Core.Enums;
using MicaWPF.Dialogs;
using ModernWpf.Controls;
using WindowsAPICodePack.Dialogs;
using System.Windows.Media.Animation;
using Flipped.Frames.Windows;
using System.Net;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.InteropServices;
public class Vars
{
    public static string exchange_code = null;
    public static string Path = null;
}
public static class ZipArchiveExtensions
{
    public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
    {
        if (!overwrite)
        {
            archive.ExtractToDirectory(destinationDirectoryName);
            return;
        }

        DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
        string destinationDirectoryFullPath = di.FullName;

        foreach (ZipArchiveEntry file in archive.Entries)
        {
            string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

            if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
            }

            if (file.Name == "")
            {// Assuming Empty for Directory
                Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                continue;
            }
            File.Delete(completeFileName);
            file.ExtractToFile(completeFileName, true);
        }
    }
}

namespace Flipped.Frames.Pages
{
    /// <summary>
    /// Interaction logic for Library.xaml
    /// </summary>
    public partial class Library : System.Windows.Controls.Page
    {
        Thread launcherThread;
        public bool running = false;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public async void Launch(string Number)
        {
            try
            {
                if (running) return;
                running = true;
                if (Vars.Path == null)
                {
                    Vars.Path = SavedData.ReadValue("Auth", $"Build{Number}Path");
                }
                string GamePath = Vars.Path;
                if (GamePath != null) // NONE THE BEST RESPONSE!
                {
                    var Backend = new Backend($"http://{LauncherIps.Backend}");
                    string discordUserId = SavedData.ReadValue("Auth", "DiscordId");
                    string hwid = SavedData.ReadValue("Auth", "hwid");

                    string exchange_code = await Backend.GetLauncherExchangeCode(discordUserId, hwid);
                    if (!string.IsNullOrEmpty(exchange_code))
                    {
                        Vars.exchange_code = exchange_code;

                    }

                    //MessageBox.Show(Path69);
                    if (File.Exists(System.IO.Path.Combine(GamePath, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe")))
                    {
                        if (Vars.exchange_code == null)
                        {
                        }
                        foreach (var proc in Process.GetProcessesByName("FlippedEAC"))
                        {
                            try
                            {
                                if (proc.MainModule.FileName.StartsWith(GamePath))
                                {
                                    proc.Kill();
                                    proc.WaitForExit();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Flipped is already running from your game path!");
                                this.Dispatcher.Invoke(() =>
                                {
                                    
                                });
                                return;
                            }
                        }
                        foreach (var proc in Process.GetProcessesByName("FortniteClient-Win64-Shipping"))
                        {
                            try
                            {
                                if (proc.MainModule.FileName.StartsWith(GamePath))
                                {
                                    proc.Kill();
                                    proc.WaitForExit();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Fortnite is already running from your game path!");
                                this.Dispatcher.Invoke(() =>
                                {
                                   
                                });
                                return;
                            }
                        }
                        WebClient Client = new WebClient();
                        //Client.DownloadFile("https://flopper.dev/Cobalt.dll", Path.Combine(Path69, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                        if (!File.Exists(System.IO.Path.Combine(GamePath, "FlippedEAC.exe")))
                        {
                            Client.DownloadFile($"https://flipped.flopper.dev/FlippedEAC.zip", Path.Combine(GamePath, "FlippedEAC.zip"));
                            System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(GamePath, "FlippedEAC.zip"), GamePath);
                            File.Delete(Path.Combine(GamePath, "FlippedEAC.zip"));
                            if (File.Exists(System.IO.Path.Combine(GamePath, "FlippedEAC.exe")) && Directory.Exists(System.IO.Path.Combine(GamePath, "EasyAntiCheat")))
                            {
                                var proc = new Process()
                                {
                                    StartInfo = new ProcessStartInfo()
                                    {
                                        Arguments = $"install 0e46812ea8ec45e9b67992db9535c553",
                                        FileName = Path.Combine(GamePath, "EasyAntiCheat", "EasyAntiCheat_EOS_Setup.exe")
                                    },
                                    EnableRaisingEvents = true
                                };
                                proc.Start();
                                proc.WaitForExit();
                                if (proc.ExitCode == 1223)
                                {
                                    MessageBox.Show("UAC request denied!");
                                    File.Delete(Path.Combine(GamePath, "FlippedEAC.exe"));
                                    DeleteDirectory(Path.Combine(GamePath, "EasyAntiCheat"));
                                    this.Dispatcher.Invoke(() =>
                                    {
                                       
                                    });
                                    return;
                                }
                                else if (proc.ExitCode != 0)
                                {
                                    MessageBox.Show("Failed to install EAC!");
                                    File.Delete(Path.Combine(GamePath, "FlippedEAC.exe"));
                                    DeleteDirectory(Path.Combine(GamePath, "EasyAntiCheat"));
                                    this.Dispatcher.Invoke(() =>
                                    {
                                      
                                    });
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Failed to download/extract EAC!");
                                this.Dispatcher.Invoke(() =>
                                {
                                   
                                });
                                return;
                            }
                        }
                        else
                        {
                            Client.DownloadFile($"https://flipped.flopper.dev/FlippedEAC.zip", Path.Combine(GamePath, "FlippedEAC.zip"));
                            var fs = new FileStream(Path.Combine(GamePath, "FlippedEAC.zip"), FileMode.Open);
                            ZipArchiveExtensions.ExtractToDirectory(new ZipArchive(fs), GamePath, true);
                            fs.Close();
                            File.Delete(Path.Combine(GamePath, "FlippedEAC.zip"));
                        }
                        if (!File.Exists(Path.Combine(GamePath, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib2.x64.dll")))
                        {
                            File.Move(Path.Combine(GamePath, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"), Path.Combine(GamePath, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib2.x64.dll"));
                        }
                        //Client.DownloadFile($"{ip}/assets/sigs.bin", Path.Combine(GamePath, "EasyAntiCheat\\Certificates", "base.bin"));
                        Client.DownloadFile($"https://flipped.flopper.dev/Starfall.dll", Path.Combine(GamePath, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                        //AntiCheat.Start(Path69);this.Dispatcher.Invoke(() =>
                        this.Dispatcher.Invoke(() =>
                        {
                          
                        });
                        Game.Start(GamePath, "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ -nullrhi", Vars.exchange_code);
                      
                        FakeAC.Start(GamePath, "FortniteClient-Win64-Shipping_EAC.exe", $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -nobe -fromfl=eac -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", "r");
                        FakeAC.Start(GamePath, "FortniteLauncher.exe", $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -nobe -fromfl=eac -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", "dsf");

                        //this.Button.Click += Button_Click_Stop;
                        //this.Button.IsEnabled = true;
                        IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                        ShowWindow(h, 6);
                        this.Dispatcher.Invoke(() =>
                        {
                           
                        });
                        try
                        {
                            Game._FortniteProcess.WaitForExit();
                        }
                        catch (Exception) { }
                        try
                        {
                            try
                            {
                                Kill();
                            }
                            catch
                            {

                            }
                            this.Dispatcher.Invoke(() =>
                            {
                              
                            });
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("An error occurred while closing Fake AC!");
                            this.Dispatcher.Invoke(() =>
                            {
                              
                            });
                        }



                        //Injector.Start(PSBasics._FortniteProcess.Id, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "EonCurl.dll"));
                        running = false;
                    }
                    else
                    {
                        MessageBox.Show("Selected path is not a valid fortnite installation!");
                        this.Dispatcher.Invoke(() =>
                        {
                         
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Please add your game path in settings!");
                    this.Dispatcher.Invoke(() =>
                    {
                    
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Kill();
                this.Dispatcher.Invoke(() =>
                {
                 
                });
            }
        }

        public static void Kill()
        {
            try
            {
                if (Game._FortniteProcess != null && !Game._FortniteProcess.HasExited) Process.GetProcessById(Game._FortniteProcess.Id).Kill();
                if (FakeAC._FNLauncherProcess != null && !FakeAC._FNLauncherProcess.HasExited) FakeAC._FNLauncherProcess.Kill();
                if (FakeAC._FNAntiCheatProcess != null && !FakeAC._FNAntiCheatProcess.HasExited) FakeAC._FNAntiCheatProcess.Kill();
            }
            catch (Exception)
            {

            }
        }
        public Library()
        {
            InitializeComponent();
            Build1.Visibility = Visibility.Collapsed;
            Build2.Visibility = Visibility.Collapsed;
            Build3.Visibility = Visibility.Collapsed;
            CheckBuilds();
            FetchInfo();
        }
        private async Task DownloadImage(string imageUrl, string localPath)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageData = await client.GetByteArrayAsync(imageUrl);

                    File.WriteAllBytes(localPath, imageData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error downloading image: {ex.Message}");
                }
            }
        }
        private void ImportBuild(object sender, RoutedEventArgs e)
        {
            Import.Visibility = Visibility.Visible;
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
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void CheckBuilds()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (SavedData.ReadValue("Auth", "Build1Path") != null)
            {
                Build1.Visibility = Visibility.Visible;
                if (SavedData.ReadValue("Auth", "Build1Season") != null)
                {
                    string seasonNumber = SavedData.ReadValue("Auth", "Build1Season");
                    string numericPart = new string(seasonNumber.Where(char.IsDigit).ToArray());
                    string formattedSeasonTitle = $"Season {numericPart}";
                    Build1Season.Text = formattedSeasonTitle;
                }
                else
                {
                    Build1Season.Text = "Fortnite Build 1";
                }
                if (SavedData.ReadValue("Auth", "Build1Season") != null)
                {
                    Build1Image.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/{SavedData.ReadValue("Auth", "Build1Season")}Poster.jpg", UriKind.Relative));
                }
                else
                {
                    Build1Image.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/OtherBuild.jpg", UriKind.Relative));
                }
                if (Build1Season.Text == "Season 14")
                {
                    Build1Status.Text = "Supported";
                }
                else
                {
                    Build1Status.Text = "Unsupported";
                }
            }
            if (SavedData.ReadValue("Auth", "Build2Path") != null)
            {
                Build2.Visibility = Visibility.Visible;
                if (SavedData.ReadValue("Auth", "Build2Season") != null)
                {
                    string seasonNumber = SavedData.ReadValue("Auth", "Build2Season");
                    string numericPart = new string(seasonNumber.Where(char.IsDigit).ToArray());
                    string formattedSeasonTitle = $"Season {numericPart}";
                    Build2Season.Text = formattedSeasonTitle;
                }
                else
                {
                    Build2Season.Text = "Fortnite Build 2";
                }
                if (SavedData.ReadValue("Auth", "Build2Season") != null)
                {
                    Build2Image.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/{SavedData.ReadValue("Auth", "Build2Season")}Poster.jpg", UriKind.Relative));
                }
                else
                {
                    Build2Image.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/OtherBuild.jpg", UriKind.Relative));
                }
                if (Build2Season.Text == "Season 19")
                {
                    Build2Status.Text = "Supported";
                }
                else
                {
                    Build2Status.Text = "Unsupported";
                }
            }
            if (SavedData.ReadValue("Auth", "Build3Path") != null)
            {
                Build3.Visibility = Visibility.Visible;
                if (SavedData.ReadValue("Auth", "Build3Season") != null)
                {
                    string seasonNumber = SavedData.ReadValue("Auth", "Build3Season");
                    string numericPart = new string(seasonNumber.Where(char.IsDigit).ToArray());
                    string formattedSeasonTitle = $"Season {numericPart}";
                    Build3Season.Text = formattedSeasonTitle;
                }
                else
                {
                    Build3Season.Text = "Fortnite Build 3";
                }
                if (SavedData.ReadValue("Auth", "Build3Season") != null)
                {
                    Build3Image.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/{SavedData.ReadValue("Auth", "Build3Season")}Poster.jpg", UriKind.Relative));
                }
                else
                {
                    Build3Image.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/OtherBuild.jpg", UriKind.Relative));
                }
                if (Build3Season.Text == "Season 19")
                {
                    Build3Status.Text = "Supported";
                }
                else
                {
                    Build3Status.Text = "Unsupported";
                }
            }
        }
        private void Build1Click(object sender, RoutedEventArgs e)
        {
            Thread launcherThread = new Thread(() => Launch("1"));
            launcherThread.Start();
            if (running == true)
            {
                Kill();
            }
        }
        private void Build1Settings(object sender, RoutedEventArgs e)
        {
            Settings1.Visibility = Visibility.Visible;
        }
        private void Build2Click(object sender, RoutedEventArgs e)
        {
            Thread launcherThread = new Thread(() => Launch("2"));
            launcherThread.Start();
            if (running == true)
            {
                Kill();
            }
        }
        private void Build2Settings(object sender, RoutedEventArgs e)
        {
            Settings2.Visibility = Visibility.Visible;
        }
        private void Build3Click(object sender, RoutedEventArgs e)
        {
            Thread launcherThread = new Thread(() => Launch("3"));
            launcherThread.Start();
            if (running == true)
            {
                Kill();
            }
        }
        private void Build3Settings(object sender, RoutedEventArgs e)
        {
            Settings3.Visibility = Visibility.Visible;
        }
        private async void PathClick(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = "Select A Fortnite Build";
            commonOpenFileDialog.Multiselect = false;
            CommonFileDialogResult commonFileDialogResult = commonOpenFileDialog.ShowDialog();

            bool flag = commonFileDialogResult == CommonFileDialogResult.Ok;
            if (flag)
            {
                string selectedFolderPath = commonOpenFileDialog.FileName;


                bool containsFortniteGame = Directory.Exists(System.IO.Path.Combine(selectedFolderPath, "FortniteGame"));
                bool containsEngine = Directory.Exists(System.IO.Path.Combine(selectedFolderPath, "Engine"));

                if (containsFortniteGame && containsEngine)
                {
                    PathBox.Text = selectedFolderPath;
                    Continue.IsEnabled = true;
                }
                else
                {
                    System.Windows.MessageBox.Show("Selected folder must contain both 'FortniteGame' and 'Engine' folders.");
                }
            }
            else
            {

            }
        }

        private string ExtractVersionNumbers(string version)
        {

            string[] parts = version.Split('-');

            if (parts.Length >= 2)
            {
                string versionPart = parts[1];
                int dotIndex = versionPart.IndexOf('.');

                if (dotIndex != -1)
                {
                    string numbersBeforeDot = versionPart.Substring(0, dotIndex);
                    return numbersBeforeDot;
                }
            }

            return version;
        }
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            SavedData.RemoveKey("Auth", "DiscordId");
            SavedData.RemoveKey("Auth", "Email");
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(appPath);
            Environment.Exit(0);
        }
        private async void ContinueClick(object sender, RoutedEventArgs e)
        {
            Add.Visibility = Visibility.Visible;
            Import.Visibility = Visibility.Collapsed;
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string result = await Flipped.Utilities.Version.GetBuildVersion(System.IO.Path.Combine(PathBox.Text, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"));
            string extractedVersion = ExtractVersionNumbers(result);
            SeasonText2.Text = $"Season {extractedVersion}";
            SeasonImage.ImageSource = new BitmapImage(new Uri($"{appDirectory}/Assets/Season{extractedVersion}.jpg", UriKind.Relative));
            AddBTN.IsEnabled = true;
        }
        private async void AddBuild(object sender, RoutedEventArgs e)
        {
            string buildsValue = SavedData.ReadValue("Auth", "Builds");
            int buildsCount = string.IsNullOrEmpty(buildsValue) ? 0 : int.Parse(buildsValue);

            if (buildsCount < 3)
            {
                string buildPathKey = $"Build{buildsCount + 1}Path";
                string buildSeasonKey = $"Build{buildsCount + 1}Season";

                SavedData.WriteToConfig("Auth", buildPathKey, PathBox.Text);

                string result = await Flipped.Utilities.Version.GetBuildVersion(System.IO.Path.Combine(PathBox.Text, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"));
                string extractedVersion = ExtractVersionNumbers(result);

                SavedData.WriteToConfig("Auth", buildSeasonKey, $"Season{extractedVersion}");

                SavedData.WriteToConfig("Auth", "Builds", (buildsCount + 1).ToString());

                CheckBuilds();
                Add.Visibility = Visibility.Collapsed;
                Import.Visibility = Visibility.Collapsed;
                AddBTN.IsEnabled = false;
                Continue.IsEnabled = false;
            }
            else
            {
                System.Windows.MessageBox.Show("Builds limit reached, please remove a build first");
                CheckBuilds();
                Add.Visibility = Visibility.Collapsed;
                Import.Visibility = Visibility.Collapsed;
                AddBTN.IsEnabled = false;
                Continue.IsEnabled = false;
            }
        }
        private void Build1Delete(object sender, RoutedEventArgs e)
        {
            SavedData.RemoveKey("Auth", "Build1Path");
            SavedData.RemoveKey("Auth", "Build1Season");

            string builds = SavedData.ReadValue("Auth", "Builds");

            if (builds == "1")
            {

                SavedData.RemoveKey("Auth", "Builds");
            }
            else if (builds == "2" || builds == "3")
            {

                SavedData.WriteToConfig("Auth", "Builds", (int.Parse(builds) - 1).ToString());
            }
            CheckBuilds();
        }
        private async void Build1Change(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = "Select A Fortnite Build";
            commonOpenFileDialog.Multiselect = false;
            CommonFileDialogResult commonFileDialogResult = commonOpenFileDialog.ShowDialog();

            bool flag = commonFileDialogResult == CommonFileDialogResult.Ok;
            if (flag)
            {
                string selectedFolderPath = commonOpenFileDialog.FileName;


                bool containsFortniteGame = Directory.Exists(System.IO.Path.Combine(selectedFolderPath, "FortniteGame"));
                bool containsEngine = Directory.Exists(System.IO.Path.Combine(selectedFolderPath, "Engine"));

                if (containsFortniteGame && containsEngine)
                {
                    string buildsValue = SavedData.ReadValue("Auth", "Builds");
                    int buildsCount = string.IsNullOrEmpty(buildsValue) ? 0 : int.Parse(buildsValue);

                    if (buildsCount < 3)
                    {
                        string buildPathKey = $"Build{buildsCount + 1}Path";
                        string buildSeasonKey = $"Build{buildsCount + 1}Season";

                        SavedData.WriteToConfig("Auth", buildPathKey, PathBox.Text);

                        string result = await Flipped.Utilities.Version.GetBuildVersion(System.IO.Path.Combine(selectedFolderPath, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"));
                        string extractedVersion = ExtractVersionNumbers(result);

                        SavedData.WriteToConfig("Auth", buildSeasonKey, $"Season{extractedVersion}");

                        SavedData.WriteToConfig("Auth", "Builds", (buildsCount + 1).ToString());

                        CheckBuilds();
                        Settings1.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Builds limit reached, please remove a build first");
                        CheckBuilds();
                        Settings1.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Selected folder must contain both 'FortniteGame' and 'Engine' folders.");
                }
            }
            else
            {

            }
        }
        private void Build1Cancel(object sender, RoutedEventArgs e)
        {
            Settings1.Visibility = Visibility.Collapsed;
        }
        private void Build2Delete(object sender, RoutedEventArgs e)
        {
            SavedData.RemoveKey("Auth", "Build2Path");
            SavedData.RemoveKey("Auth", "Build2Season");

            string builds = SavedData.ReadValue("Auth", "Builds");

            if (builds == "1")
            {

                SavedData.RemoveKey("Auth", "Builds");
            }
            else if (builds == "2" || builds == "3")
            {

                SavedData.WriteToConfig("Auth", "Builds", (int.Parse(builds) - 1).ToString());
            }
            CheckBuilds();
        }
        private async void Build2Change(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = "Select A Fortnite Build";
            commonOpenFileDialog.Multiselect = false;
            CommonFileDialogResult commonFileDialogResult = commonOpenFileDialog.ShowDialog();

            bool flag = commonFileDialogResult == CommonFileDialogResult.Ok;
            if (flag)
            {
                string selectedFolderPath = commonOpenFileDialog.FileName;


                bool containsFortniteGame = Directory.Exists(System.IO.Path.Combine(selectedFolderPath, "FortniteGame"));
                bool containsEngine = Directory.Exists(System.IO.Path.Combine(selectedFolderPath, "Engine"));

                if (containsFortniteGame && containsEngine)
                {
                    string buildsValue = SavedData.ReadValue("Auth", "Builds");
                    int buildsCount = string.IsNullOrEmpty(buildsValue) ? 0 : int.Parse(buildsValue);

                    if (buildsCount < 3)
                    {
                        string buildPathKey = $"Build{buildsCount + 1}Path";
                        string buildSeasonKey = $"Build{buildsCount + 1}Season";

                        SavedData.WriteToConfig("Auth", buildPathKey, PathBox.Text);

                        string result = await Flipped.Utilities.Version.GetBuildVersion(System.IO.Path.Combine(selectedFolderPath, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"));
                        string extractedVersion = ExtractVersionNumbers(result);

                        SavedData.WriteToConfig("Auth", buildSeasonKey, $"Season{extractedVersion}");

                        SavedData.WriteToConfig("Auth", "Builds", (buildsCount + 1).ToString());

                        CheckBuilds();
                        Settings2.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Builds limit reached, please remove a build first");
                        CheckBuilds();
                        Settings2.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Selected folder must contain both 'FortniteGame' and 'Engine' folders.");
                }
            }
            else
            {

            }
        }
        private void Build2Cancel(object sender, RoutedEventArgs e)
        {
            Settings2.Visibility = Visibility.Collapsed;
        }
        private void Build3Delete(object sender, RoutedEventArgs e)
        {
            SavedData.RemoveKey("Auth", "Build3Path");
            SavedData.RemoveKey("Auth", "Build3Season");

            string builds = SavedData.ReadValue("Auth", "Builds");

            if (builds == "1")
            {

                SavedData.RemoveKey("Auth", "Builds");
            }
            else if (builds == "2" || builds == "3")
            {

                SavedData.WriteToConfig("Auth", "Builds", (int.Parse(builds) - 1).ToString());
            }
            CheckBuilds();
        }
        private async void Build3Change(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = "Select A Fortnite Build";
            commonOpenFileDialog.Multiselect = false;
            CommonFileDialogResult commonFileDialogResult = commonOpenFileDialog.ShowDialog();

            bool flag = commonFileDialogResult == CommonFileDialogResult.Ok;
            if (flag)
            {
                string selectedFolderPath = commonOpenFileDialog.FileName;


                bool containsFortniteGame = Directory.Exists(System.IO.Path.Combine(selectedFolderPath, "FortniteGame"));
                bool containsEngine = Directory.Exists(System.IO.Path.Combine(selectedFolderPath, "Engine"));

                if (containsFortniteGame && containsEngine)
                {
                    string buildsValue = SavedData.ReadValue("Auth", "Builds");
                    int buildsCount = string.IsNullOrEmpty(buildsValue) ? 0 : int.Parse(buildsValue);

                    if (buildsCount < 3)
                    {
                        string buildPathKey = $"Build{buildsCount + 1}Path";
                        string buildSeasonKey = $"Build{buildsCount + 1}Season";

                        SavedData.WriteToConfig("Auth", buildPathKey, PathBox.Text);

                        string result = await Flipped.Utilities.Version.GetBuildVersion(System.IO.Path.Combine(selectedFolderPath, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"));
                        string extractedVersion = ExtractVersionNumbers(result);

                        SavedData.WriteToConfig("Auth", buildSeasonKey, $"Season{extractedVersion}");

                        SavedData.WriteToConfig("Auth", "Builds", (buildsCount + 1).ToString());

                        CheckBuilds();
                        Settings3.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Builds limit reached, please remove a build first");
                        CheckBuilds();
                        Settings3.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Selected folder must contain both 'FortniteGame' and 'Engine' folders.");
                }
            }
            else
            {

            }
        }
        private void Build3Cancel(object sender, RoutedEventArgs e)
        {
            Settings3.Visibility = Visibility.Collapsed;
        }
    }
}
