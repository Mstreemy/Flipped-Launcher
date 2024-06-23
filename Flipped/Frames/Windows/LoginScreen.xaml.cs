using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Flipped;
using Flipped.Utilities;
using Microsoft.Web.WebView2.Core;

namespace Flipped.Frames.Windows
{
    public partial class LoginScreen : Window
    {
        private bool isDragging = false;
        private Point startPoint;

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
        private void TopBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void LoginClick(object sender, RoutedEventArgs e)
        {
            var Backend = new Backend($"http://{LauncherIps.Backend}");
            string response = await Backend.LoginToken(TokenBox.Text);

            if (response != null)
            {
                if (response != "false")
                {
                    string discordUserId = await Backend.GetLauncherDiscordId(TokenBox.Text);
                    SavedData.WriteToConfig("Auth", "DiscordId", discordUserId);

                    string hwid = GetHardwareId();

                    SavedData.WriteToConfig("Auth", "DiscordId", discordUserId);
                    SavedData.WriteToConfig("Auth", "hwid", hwid);

                    await Backend.GetLauncherHWIDCheck(discordUserId, hwid);

                    try
                    {
                        string savedDiscordId = SavedData.ReadValue("Auth", "DiscordId");
                        string discordCallbackId = await Backend.HWIDBanCheck(hwid);

                        if (discordCallbackId.Trim('"') == savedDiscordId)
                        {
                            ValidBox.Visibility = Visibility.Visible;
                            ValidMSG.Text = "Logged in! Loading all assets...";
                            ErrorBox.Visibility = Visibility.Collapsed;
                            await Task.Delay(1500);
                            var Home = new MainWindow();
                            Home.Show();
                            this.Close();
                        }
                        else
                        {
                            ErrorBox.Visibility = Visibility.Visible;
                            ErrorMSG.Text = "Error: You are Banned.";
                            ValidBox.Visibility = Visibility.Collapsed;
                            SavedData.RemoveKey("Auth", "DiscordId");
                            SavedData.RemoveKey("Auth", "hwid");
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorBox.Visibility = Visibility.Visible;
                        ErrorMSG.Text = $"An error occurred: {ex.Message}";
                        ValidBox.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    ErrorBox.Visibility = Visibility.Visible;
                    ErrorMSG.Text = "Login failed, please try again!";
                    ValidBox.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                ErrorBox.Visibility = Visibility.Visible;
                ErrorMSG.Text = "Login failed, please try again!";
                ValidBox.Visibility = Visibility.Collapsed;
            }
        }

        private string GetHardwareId()
        {
            try
            {
                string powerShellScript = @"
            $motherboardId = (Get-WmiObject Win32_BaseBoard).SerialNumber
            $userFolder = $env:UserName
            $cpuId = (Get-WmiObject Win32_Processor).ProcessorId
            $combinedId = $motherboardId + '_' + $userFolder + '_' + $cpuId
            Write-Output $combinedId
            ";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"" + powerShellScript + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string hwid = reader.ReadToEnd();
                        return hwid.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception: {ex.Message}");
                return null;
            }
        }

    }
}
