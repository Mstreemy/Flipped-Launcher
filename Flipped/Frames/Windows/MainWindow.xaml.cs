using System.Diagnostics;
using System.Text;
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

namespace Flipped.Frames.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HomeSymbol.Filled = true;
            HomeText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            HomeSymbol.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            HomeText.FontWeight = FontWeights.Bold;
            NavigateWithTransition(new Uri("/Frames/Pages/Home.xaml", UriKind.RelativeOrAbsolute));
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
            // Add any additional logic if needed
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            // Add any additional logic if needed
        }
        // Window Clicks
        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ResetButtonAppearance()
        {
            HomeSymbol.Filled = false;
            HomeText.Foreground = Brushes.White;
            HomeSymbol.Foreground = Brushes.White;
            HomeText.FontWeight = FontWeights.Normal;

            LibrarySymbol.Filled = false;
            LibraryText.Foreground = Brushes.White;
            LibrarySymbol.Foreground = Brushes.White;
            LibraryText.FontWeight = FontWeights.Normal;

            SettingsSymbol.Filled = false;
            SettingsText.Foreground = Brushes.White;
            SettingsSymbol.Foreground = Brushes.White;
            SettingsText.FontWeight = FontWeights.Normal;

            ShopSymbol.Filled = false;
            ShopText.Foreground = Brushes.White;
            ShopSymbol.Foreground = Brushes.White;
            ShopText.FontWeight = FontWeights.Normal;
        }
        public void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonAppearance();
            HomeSymbol.Filled = true;
            HomeText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            HomeSymbol.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            HomeText.FontWeight = FontWeights.Bold;
            NavigateWithTransition(new Uri("/Frames/Pages/Home.xaml", UriKind.RelativeOrAbsolute));
        }
        private void LibraryButton_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonAppearance();
            LibrarySymbol.Filled = true;
            LibraryText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            LibrarySymbol.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            LibraryText.FontWeight = FontWeights.Bold;
            NavigateWithTransition(new Uri("/Frames/Pages/Library.xaml", UriKind.RelativeOrAbsolute));
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonAppearance();
            SettingsSymbol.Filled = true;
            SettingsText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            SettingsSymbol.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            SettingsText.FontWeight = FontWeights.Bold;
            SettingsContentFrame.Visibility = Visibility.Visible;
            NavigateWithTransition2(new Uri("/Frames/Pages/Settings.xaml", UriKind.RelativeOrAbsolute));
        }
        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonAppearance();
            ShopSymbol.Filled = true;
            ShopText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            ShopSymbol.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7864e4"));
            ShopText.FontWeight = FontWeights.Bold;
            NavigateWithTransition(new Uri("/Frames/Pages/Shop.xaml", UriKind.RelativeOrAbsolute));
        }
        private void HomeEnter(object sender, MouseEventArgs e)
        {
            Home.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303030"));
        }
        private void LibraryEnter(object sender, MouseEventArgs e)
        {
            Library.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303030"));
        }
        private void SettingsEnter(object sender, MouseEventArgs e)
        {
            Settings.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303030"));
        }
        private void ShopEnter(object sender, MouseEventArgs e)
        {
            Shop.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303030"));
        }
        private void HomeLeave(object sender, MouseEventArgs e)
        {
            Home.Background = Brushes.Transparent;
        }
        private void LibraryLeave(object sender, MouseEventArgs e)
        {
            Library.Background = Brushes.Transparent;
        }
        private void SettingsLeave(object sender, MouseEventArgs e)
        {
            Settings.Background = Brushes.Transparent;

        }
        private void ShopLeave(object sender, MouseEventArgs e)
        {
            Shop.Background = Brushes.Transparent;
        }
        private void NavigateWithTransition(Uri uri)
        {
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.2),
            };

            fadeOutAnimation.Completed += (sender, e) =>
            {
                ContentFrame.Navigate(uri);

                var fadeInAnimation = new DoubleAnimation
                {
                    From = 0.0,
                    To = 1.0,
                    Duration = TimeSpan.FromSeconds(0.2),
                };

                ContentFrame.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
            };
            ContentFrame.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }
        public void LeaveSettings()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
        private void NavigateWithTransition2(Uri uri)
        {
           
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.2),
            };

            fadeOutAnimation.Completed += (sender, e) =>
            {
                SettingsContentFrame.Navigate(uri);

                var fadeInAnimation = new DoubleAnimation
                {
                    From = 0.0,
                    To = 1.0,
                    Duration = TimeSpan.FromSeconds(0.2),
                };

                SettingsContentFrame.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
            };
            SettingsContentFrame.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }
    }
}