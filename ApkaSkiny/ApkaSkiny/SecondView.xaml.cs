using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny.ViewModels;
using System.Windows.Input; // Add this line for MouseEventArgs
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;
using ApkaSkiny.View;

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
        private async void OnDisplayStatistics(object sender, RoutedEventArgs e)
        {
            // Ensure you're calling the method to update the statistics
            await Task.Run(() => _viewModel.DisplayStatistics());
        }


        // Sortowanie po cenie
        private void OnSortByPrice(object sender, RoutedEventArgs e)
        {
            _viewModel.DisplaySkinsSortedByPriceAsync();  // Wywołanie metody z ViewModel
        }

        private async void OnSortByCollection(object sender, RoutedEventArgs e)
        {
            // Ask for the collection name
            InputWindow collectionInputWindow = new InputWindow("Enter the collection name to filter by:", "Collection Name");
            bool? collectionResult = collectionInputWindow.ShowDialog();

            string collection = null;
            if (collectionResult == true)
            {
                collection = collectionInputWindow.UserInput;

                // If the user input is empty, we can show a message or default behavior
                if (string.IsNullOrEmpty(collection))
                {
                    MessageBox.Show("Please enter a valid collection name.");
                    return;
                }
            }

            // Call the method from ViewModel to display skins by the entered collection name
            await _viewModel.DisplaySkinsByCollectionAsync(collection);

            // Optionally, show a message if no skins are found for the entered collection
            var filteredSkins = _viewModel.Skins.Where(s => s.Collection.Equals(collection, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filteredSkins.Any())
            {
                MessageBox.Show($"No skins found for the collection: {collection}");
            }
        }


        private async void OnSortByWeaponType(object sender, RoutedEventArgs e)
        {
            // Ask for the weapon type
            InputWindow weaponTypeInputWindow = new InputWindow("Enter the weapon type to filter by:", "Weapon Type");
            bool? weaponTypeResult = weaponTypeInputWindow.ShowDialog();

            string weaponType = null;
            if (weaponTypeResult == true)
            {
                weaponType = weaponTypeInputWindow.UserInput;

                // If the user input is empty, we can show a message or default behavior
                if (string.IsNullOrEmpty(weaponType))
                {
                    MessageBox.Show("Please enter a valid weapon type.");
                    return;
                }
            }

            // Call the method from ViewModel to display skins by the entered weapon type
            await _viewModel.DisplaySkinsByWeaponTypeAsync(weaponType);

            // Optionally, show a message if no skins are found for the entered weapon type
            var filteredSkins = _viewModel.Skins.Where(s => s.WeaponType.Equals(weaponType, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filteredSkins.Any())
            {
                MessageBox.Show($"No skins found for the weapon type: {weaponType}");
            }
        }

        private async void OnSortByChosenPrice(object sender, RoutedEventArgs e)
        {
            // Ask for minimum price
            InputWindow minPriceInputWindow = new InputWindow("Enter the minimum price (leave blank for no filter):", "Min Price");
            bool? minPriceResult = minPriceInputWindow.ShowDialog();

            decimal? minPrice = null;
            if (minPriceResult == true)
            {
                string minPriceUserInput = minPriceInputWindow.UserInput;

                // Check if the input is not empty and try to parse the minimum price
                if (!string.IsNullOrEmpty(minPriceUserInput))
                {
                    if (decimal.TryParse(minPriceUserInput, out decimal minPriceValue))
                    {
                        if (minPriceValue >= 0) // Ensure the minimum price is not negative
                        {
                            minPrice = minPriceValue;
                        }
                        else
                        {
                            MessageBox.Show("Minimum price cannot be negative. Please enter a valid number.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid minimum price input. Please enter a valid number.");
                        return;
                    }
                }
            }

            // Ask for maximum price
            InputWindow maxPriceInputWindow = new InputWindow("Enter the maximum price (leave blank for no filter):", "Max Price");
            bool? maxPriceResult = maxPriceInputWindow.ShowDialog();

            decimal? maxPrice = null;
            if (maxPriceResult == true)
            {
                string maxPriceUserInput = maxPriceInputWindow.UserInput;

                // Check if the input is not empty and try to parse the maximum price
                if (!string.IsNullOrEmpty(maxPriceUserInput))
                {
                    if (decimal.TryParse(maxPriceUserInput, out decimal maxPriceValue))
                    {
                        if (maxPriceValue >= 0) // Ensure the maximum price is not negative
                        {
                            maxPrice = maxPriceValue;
                        }
                        else
                        {
                            MessageBox.Show("Maximum price cannot be negative. Please enter a valid number.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid maximum price input. Please enter a valid number.");
                        return;
                    }
                }
            }

            // Once we have both prices (or null values for no filtering), display skins by price range
            await _viewModel.DisplaySkinsByChosenPriceAsync(minPrice, maxPrice);
        }

        // Wyszukiwanie po nazwie
        private async void OnSearchByName(object sender, RoutedEventArgs e)
        {
            // Ask for the skin name
            InputWindow nameInputWindow = new InputWindow("Enter the skin name to search for:", "Skin Name");
            bool? nameResult = nameInputWindow.ShowDialog();

            string searchTerm = null;
            if (nameResult == true)
            {
                searchTerm = nameInputWindow.UserInput;

                // If the user input is empty, we will set searchTerm to null to show all skins
                if (string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = null;
                }
            }

            // If searchTerm is null or empty, we display all skins
            List<Skin> filteredSkins;
            if (string.IsNullOrEmpty(searchTerm))
            {
                filteredSkins = _viewModel.Skins.ToList(); // Show all skins if searchTerm is null or empty
            }
            else
            {
                // Filter skins where name contains searchTerm (case-insensitive)
                filteredSkins = _viewModel.Skins
                    .Where(s => s.Name != null && s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Clear the existing skins and add the filtered skins
            _viewModel.Skins.Clear();
            foreach (var skin in filteredSkins)
            {
                _viewModel.Skins.Add(skin);  // Add the filtered skins back to the collection
            }

            // Optionally, display a message if no skins are found and searchTerm is not null
            if (!string.IsNullOrEmpty(searchTerm) && !filteredSkins.Any())
            {
                MessageBox.Show($"No skins found matching the name: {searchTerm}");
            }
        }



        // Event handler to add a new skin
        private void OnAddNewSkin(object sender, RoutedEventArgs e)
        {
            // Ask for skin name
            InputWindow nameInputWindow = new InputWindow("Enter skin name:", "Skin Name");
            bool? nameResult = nameInputWindow.ShowDialog();
            string name = null;
            if (nameResult == true)
            {
                name = nameInputWindow.UserInput;
            }

            // Ask for collection name
            InputWindow collectionInputWindow = new InputWindow("Enter collection name:", "Collection Name");
            bool? collectionResult = collectionInputWindow.ShowDialog();
            string collection = null;
            if (collectionResult == true)
            {
                collection = collectionInputWindow.UserInput;
            }

            // Ask for weapon type
            InputWindow weaponTypeInputWindow = new InputWindow("Enter weapon type:", "Weapon Type");
            bool? weaponTypeResult = weaponTypeInputWindow.ShowDialog();
            string weaponType = null;
            if (weaponTypeResult == true)
            {
                weaponType = weaponTypeInputWindow.UserInput;
            }

            // Ask for price
            InputWindow priceInputWindow = new InputWindow("Enter price of the skin:", "Price");
            bool? priceResult = priceInputWindow.ShowDialog();
            decimal price = 0;
            if (priceResult == true && decimal.TryParse(priceInputWindow.UserInput, out price) && price >= 0)
            {
                MessageBox.Show("Maximum price cannot be negative. Please enter a valid number.");
                return;
            }
            else
            {
                MessageBox.Show("Invalid price input.");
                return;
            }

            // Ask for side (Terrorists, Counter-Terrorists, or Both)
            InputWindow sideInputWindow = new InputWindow("Enter side (Terrorists, Counter-Terrorists, or Both):", "Side");
            bool? sideResult = sideInputWindow.ShowDialog();
            string side = null;
            if (sideResult == true)
            {
                side = sideInputWindow.UserInput;
            }

            // Ask for weapon category
            InputWindow weaponCategoryInputWindow = new InputWindow("Enter weapon category:", "Weapon Category");
            bool? weaponCategoryResult = weaponCategoryInputWindow.ShowDialog();
            string weaponCategory = null;
            if (weaponCategoryResult == true)
            {
                weaponCategory = weaponCategoryInputWindow.UserInput;
            }

            // Add the new skin to the ViewModel
            _viewModel.AddNewSkin(name, collection, weaponType, price, side, weaponCategory);
        }

        private async void OnRemoveSkin(object sender, RoutedEventArgs e)
        {
            // Synchronizuj dane w ViewModel
            await _viewModel.DisplaySkinsAsync(); // Załaduj skórki do ViewModel, jeśli jeszcze nie są załadowane

            // Pobierz wszystkie skiny z ViewModel
            var skins = _viewModel.Skins;

            // Sprawdź, czy lista skinów nie jest pusta
            if (!skins.Any())
            {
                MessageBox.Show("Brak skórek do usunięcia!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Przygotowanie listy wyboru w formacie "Name (WeaponType)"
            var skinChoices = skins.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            // Wywołanie okna dialogowego z wyborem skinu
            var dialog = new SelectSkinDialog("Wybierz skin do usunięcia:", skinChoices);
            if (dialog.ShowDialog() == true)
            {
                // Rozdzielenie nazwy i typu broni
                var selectedSkinData = dialog.SelectedSkin?.Split('(');
                if (selectedSkinData == null || selectedSkinData.Length < 2)
                {
                    MessageBox.Show("Niepoprawny wybór skina!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedSkinName = selectedSkinData[0].Trim();

                // Znalezienie wybranego skina
                var skinToRemove = skins.FirstOrDefault(s => s.Name.Equals(selectedSkinName, StringComparison.OrdinalIgnoreCase));
                if (skinToRemove != null)
                {
                    _viewModel.RemoveSkin(skinToRemove); // Usunięcie z ViewModel
                    MessageBox.Show($"Skin \"{skinToRemove.Name}\" został usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nie znaleziono wybranego skina do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private async void OnAddSkinToFavorites(object sender, RoutedEventArgs e)
        {
            // Synchronizuj dane w ViewModel
            await _viewModel.DisplaySkinsAsync(); // Załaduj skórki do ViewModel, jeśli jeszcze nie są załadowane

            // Pobierz wszystkie skiny z ViewModel
            var skins = _viewModel.Skins;

            // Sprawdź, czy lista skinów nie jest pusta
            if (!skins.Any())
            {
                MessageBox.Show("Brak skórek do dodania do ulubionych!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Przygotowanie listy wyboru w formacie "Name (WeaponType)"
            var skinChoices = skins.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            // Wywołanie okna dialogowego z wyborem skinu
            var dialog = new SelectSkinDialog("Wybierz skin do dodania do ulubionych:", skinChoices);
            if (dialog.ShowDialog() == true)
            {
                // Rozdzielenie nazwy i typu broni
                var selectedSkinData = dialog.SelectedSkin?.Split('(');
                if (selectedSkinData == null || selectedSkinData.Length < 2)
                {
                    MessageBox.Show("Niepoprawny wybór skina!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedSkinName = selectedSkinData[0].Trim();

                // Znalezienie wybranego skina
                var skinToAdd = skins.FirstOrDefault(s => s.Name.Equals(selectedSkinName, StringComparison.OrdinalIgnoreCase));
                if (skinToAdd != null)
                {
                    // Dodaj skin do ulubionych
                    _viewModel.AddSkinToFavorites(skinToAdd); // Dodanie skina do ulubionych w ViewModel
                    MessageBox.Show($"Skin \"{skinToAdd.Name}\" został dodany do ulubionych.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nie znaleziono wybranego skina do dodania do ulubionych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void OnRemoveSkinFromFavorites(object sender, RoutedEventArgs e)
        {
            // Synchronizuj dane w ViewModel
            await _viewModel.DisplayFavoriteSkinsAsync(); // Załaduj ulubione skórki do ViewModel, jeśli jeszcze nie są załadowane

            // Pobierz wszystkie ulubione skiny z ViewModel
            var favoriteSkins = _viewModel.Skins.Where(s => s.IsFavorite).ToList();

            // Sprawdź, czy lista ulubionych skinów nie jest pusta
            if (!favoriteSkins.Any())
            {
                MessageBox.Show("Brak ulubionych skórek do usunięcia!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Przygotowanie listy wyboru w formacie "Name (WeaponType)"
            var skinChoices = favoriteSkins.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            // Wywołanie okna dialogowego z wyborem skina
            var dialog = new SelectSkinDialog("Wybierz skin do usunięcia z ulubionych:", skinChoices);
            if (dialog.ShowDialog() == true)
            {
                // Rozdzielenie nazwy i typu broni
                var selectedSkinData = dialog.SelectedSkin?.Split('(');
                if (selectedSkinData == null || selectedSkinData.Length < 2)
                {
                    MessageBox.Show("Niepoprawny wybór skina!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedSkinName = selectedSkinData[0].Trim();

                // Znalezienie wybranego skina
                var skinToRemove = favoriteSkins.FirstOrDefault(s => s.Name.Equals(selectedSkinName, StringComparison.OrdinalIgnoreCase));
                if (skinToRemove != null)
                {
                    // Usuń skin z ulubionych
                    _viewModel.RemoveSkinFromFavorites(skinToRemove); // Usunięcie skina z ulubionych w ViewModel
                    MessageBox.Show($"Skin \"{skinToRemove.Name}\" został usunięty z ulubionych.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nie znaleziono wybranego skina do usunięcia z ulubionych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
