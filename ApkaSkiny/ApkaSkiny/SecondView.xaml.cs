using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny.ViewModels;
using ApkaSkiny.View;
using ApkaSkiny;
using System.Windows.Input; // Add this line for MouseEventArgs
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Linq;
namespace ApkaSkiny
{
    public partial class SecondView : Window
    {
        private SkinViewModel _viewModel;
        private Controller _controller;

        public SecondView(Controller controller)
        {
            InitializeComponent();
            _controller = controller;

            _viewModel = new SkinViewModel();  // Instancja ViewModel
            DataContext = _viewModel;         // Ustawienie ViewModel jako DataContext
        }

        // Wyświetlanie wszystkich skinów
        private void OnDisplayAllSkins(object sender, RoutedEventArgs e)
        {
            _viewModel.DisplaySkinsAsync();  // Wywołanie metody z ViewModel
        }

        // Wyświetlanie ulubionych skinów
        private void OnDisplayFavoriteSkins(object sender, RoutedEventArgs e)
        {
            _viewModel.DisplayFavoriteSkinsAsync();  // Wywołanie metody z ViewModel
        }

        // Dodawanie nowego skina
        private void OnAddNewSkin(object sender, RoutedEventArgs e)
        {
            // Logika dodawania nowego skina
        }

        // Usuwanie skina
        private void OnRemoveSkin(object sender, RoutedEventArgs e)
        {
            // Logika usuwania skina
        }

        // Dodawanie skina do ulubionych
        private void OnAddSkinToFavorites(object sender, RoutedEventArgs e)
        {
            // Logika dodawania skina do ulubionych
        }

        // Usuwanie skina z ulubionych
        private void OnRemoveSkinFromFavorites(object sender, RoutedEventArgs e)
        {
            // Logika usuwania skina z ulubionych
        }

        // Sortowanie po cenie
        private void OnSortByPrice(object sender, RoutedEventArgs e)
        {
            // Logika sortowania po cenie
        }

        // Sortowanie po kolekcji
        private void OnSortByCollection(object sender, RoutedEventArgs e)
        {
            // Logika sortowania po kolekcji
        }

        // Sortowanie po typie broni
        private void OnSortByWeaponType(object sender, RoutedEventArgs e)
        {
            // Logika sortowania po typie broni
        }

        // Sortowanie po wybranej cenie
        private void OnSortByChosenPrice(object sender, RoutedEventArgs e)
        {
            // Logika sortowania po wybranej cenie
        }

        // Wyszukiwanie po nazwie
        private void OnSearchByName(object sender, RoutedEventArgs e)
        {
            // Logika wyszukiwania po nazwie
        }

        // Wyświetlanie statystyk
        private void OnShowStatistics(object sender, RoutedEventArgs e)
        {
            // Logika wyświetlania statystyk
        }

        // Zamykanie aplikacji
        private void OnExitApp(object sender, RoutedEventArgs e)
        {
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
