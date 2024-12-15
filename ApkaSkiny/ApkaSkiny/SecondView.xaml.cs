using ApkaSkiny.Models;
using ApkaSkiny.View;
using System.Windows;
using System.Windows.Input; // Add this line for MouseEventArgs
using System.Windows.Media;
using System.Windows.Controls;
namespace ApkaSkiny
{
    public partial class SecondView : Window
    {
        private readonly Controller _controller;

        public SecondView(Controller controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        // Display all skins
        private void OnDisplayAllSkins(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.DisplaySkins();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Display favorite skins
        private void OnDisplayFavoriteSkins(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.DisplayFavoriteSkins();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Add a new skin
        private void OnAddNewSkin(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.AddNewSkin();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Remove a skin
        private void OnRemoveSkin(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.RemoveSkin();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Add a skin to favorites
        private void OnAddSkinToFavorites(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.AddSkinToFavorites();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Remove a skin from favorites
        private void OnRemoveSkinFromFavorites(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.RemoveSkinFromFavorites();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Sort by price
        private void OnSortByPrice(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.DisplaySkinsSortedByPrice();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Sort by collection
        private void OnSortByCollection(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.DisplaySkinsByCollection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Sort by weapon type
        private void OnSortByWeaponType(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.DisplaySkinsByWeaponType();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Sort by chosen price
        private void OnSortByChosenPrice(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.DisplaySkinsByChosenPrice();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Search by name
        private void OnSearchByName(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.SearchSkinsByName();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Show statistics
        private void OnShowStatistics(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.DisplayStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Exit the application
        private void OnExitApp(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Zamykanie aplikacji...");
            Application.Current.Shutdown();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = (SolidColorBrush)FindResource("ButtonHoverColor"); // Lighter pink on hover
            }
        }

        // MouseLeave event to reset background after hover
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = (SolidColorBrush)FindResource("ButtonBackgroundColor"); // Reset to original color
            }
        }
    }
}
