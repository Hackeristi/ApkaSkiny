using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny.ViewModels;
using System.Windows.Input; 
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

            _viewModel = new SkinViewModel();
            DataContext = _viewModel;
        }

        private void OnDisplayAllSkins(object sender, RoutedEventArgs e)
        {
            _viewModel.DisplaySkinsAsync();
        }

        private void OnDisplayFavoriteSkins(object sender, RoutedEventArgs e)
        {
            _viewModel.DisplayFavoriteSkinsAsync();
        }
        private async void OnDisplayStatistics(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => _viewModel.DisplayStatistics());
        }


        private void OnSortByPrice(object sender, RoutedEventArgs e)
        {
            _viewModel.DisplaySkinsSortedByPriceAsync();
        }

        private async void OnSortByCollection(object sender, RoutedEventArgs e)
        {
            InputWindow collectionInputWindow = new InputWindow("Wpisz nazwę kolekcji, aby przefiltrować:", "Nazwa kolekcji");
            bool? collectionResult = collectionInputWindow.ShowDialog();

            string collection = null;
            if (collectionResult == true)
            {
                collection = collectionInputWindow.UserInput;

                if (string.IsNullOrEmpty(collection))
                {
                    MessageBox.Show("Podaj poprawną nazwę kolekcji.");
                    return;
                }
            }

            await _viewModel.DisplaySkinsByCollectionAsync(collection);

            var filteredSkins = _viewModel.Skins.Where(s => s.Collection.Equals(collection, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filteredSkins.Any())
            {
                MessageBox.Show($"Nie znaleziono skórek dla kolekcji: {collection}");
            }
        }


        private async void OnSortByWeaponType(object sender, RoutedEventArgs e)
        {
            InputWindow weaponTypeInputWindow = new InputWindow("Wpisz typ broni, aby przefiltrować:", "Typ broni");
            bool? weaponTypeResult = weaponTypeInputWindow.ShowDialog();

            string weaponType = null;
            if (weaponTypeResult == true)
            {
                weaponType = weaponTypeInputWindow.UserInput;

                if (string.IsNullOrEmpty(weaponType))
                {
                    MessageBox.Show("Podaj poprawny typ broni.");
                    return;
                }
            }

            await _viewModel.DisplaySkinsByWeaponTypeAsync(weaponType);

            var filteredSkins = _viewModel.Skins.Where(s => s.WeaponType.Equals(weaponType, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filteredSkins.Any())
            {
                MessageBox.Show($"Nie znaleziono skórek dla typu broni: {weaponType}");
            }
        }

        private async void OnSortByChosenPrice(object sender, RoutedEventArgs e)
        {
            InputWindow minPriceInputWindow = new InputWindow("Wpisz minimalną cenę (zostaw puste, aby pominąć):", "Minimalna cena");
            bool? minPriceResult = minPriceInputWindow.ShowDialog();

            decimal? minPrice = null;
            if (minPriceResult == true)
            {
                string minPriceUserInput = minPriceInputWindow.UserInput;

                if (!string.IsNullOrEmpty(minPriceUserInput))
                {
                    if (decimal.TryParse(minPriceUserInput, out decimal minPriceValue))
                    {
                        if (minPriceValue >= 0) 
                        {
                            minPrice = minPriceValue;
                        }
                        else
                        {
                            MessageBox.Show("Minimalna cena nie może być ujemna.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Podano nieprawidłową wartość minimalnej ceny.");
                        return;
                    }
                }
            }

            InputWindow maxPriceInputWindow = new InputWindow("Wpisz maksymalną cenę (zostaw puste, aby pominąć):", "Maksymalna cena");
            bool? maxPriceResult = maxPriceInputWindow.ShowDialog();

            decimal? maxPrice = null;
            if (maxPriceResult == true)
            {
                string maxPriceUserInput = maxPriceInputWindow.UserInput;

                if (!string.IsNullOrEmpty(maxPriceUserInput))
                {
                    if (decimal.TryParse(maxPriceUserInput, out decimal maxPriceValue))
                    {
                        if (maxPriceValue >= 0)
                        {
                            maxPrice = maxPriceValue;
                        }
                        else
                        {
                            MessageBox.Show("Maksymalna cena nie może być ujemna.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Podano nieprawidłową wartość maksymalnej ceny.");
                        return;
                    }
                }
            }

            await _viewModel.DisplaySkinsByChosenPriceAsync(minPrice, maxPrice);
        }

        private async void OnSearchByName(object sender, RoutedEventArgs e)
        {
            InputWindow nameInputWindow = new InputWindow("Wpisz nazwę skina:", "Nazwa skina");
            bool? nameResult = nameInputWindow.ShowDialog();

            string searchTerm = null;
            if (nameResult == true)
            {
                searchTerm = nameInputWindow.UserInput;

                if (string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = null;
                }
            }

            List<Skin> filteredSkins;
            if (string.IsNullOrEmpty(searchTerm))
            {
                filteredSkins = _viewModel.Skins.ToList();
            }
            else
            {
                filteredSkins = _viewModel.Skins
                    .Where(s => s.Name != null && s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            _viewModel.Skins.Clear();
            foreach (var skin in filteredSkins)
            {
                _viewModel.Skins.Add(skin);  
            }

            if (!string.IsNullOrEmpty(searchTerm) && !filteredSkins.Any())
            {
                MessageBox.Show($"Nie znaleziono skórek o nazwie: {searchTerm}");
            }
        }
        private void OnAddNewSkin(object sender, RoutedEventArgs e)
        {
            string name = null;
            while (string.IsNullOrEmpty(name))
            {
                InputWindow nameInputWindow = new InputWindow("Wpisz nazwę skina:", "Nazwa skina");
                bool? nameResult = nameInputWindow.ShowDialog();
                if (nameResult == true)
                {
                    name = nameInputWindow.UserInput;
                    if (string.IsNullOrEmpty(name))
                    {
                        MessageBox.Show("Nazwa skina nie może być pusta.");
                    }
                }
                else
                {
                    MessageBox.Show("Dodawanie skina zostało anulowane.");
                    return;
                }
            }

            string collection = null;
            while (string.IsNullOrEmpty(collection))
            {
                InputWindow collectionInputWindow = new InputWindow("Wpisz nazwę kolekcji:", "Nazwa kolekcji");
                bool? collectionResult = collectionInputWindow.ShowDialog();
                if (collectionResult == true)
                {
                    collection = collectionInputWindow.UserInput;
                    if (string.IsNullOrEmpty(collection))
                    {
                        MessageBox.Show("Nazwa kolekcji nie może być pusta.");
                    }
                }
                else
                {
                    MessageBox.Show("Dodawanie skina zostało anulowane.");
                    return;
                }
            }

            string weaponType = null;
            while (string.IsNullOrEmpty(weaponType))
            {
                InputWindow weaponTypeInputWindow = new InputWindow("Wpisz typ broni:", "Typ broni");
                bool? weaponTypeResult = weaponTypeInputWindow.ShowDialog();
                if (weaponTypeResult == true)
                {
                    weaponType = weaponTypeInputWindow.UserInput;
                    if (string.IsNullOrEmpty(weaponType))
                    {
                        MessageBox.Show("Typ broni nie może być pusty.");
                    }
                }
                else
                {
                    MessageBox.Show("Dodawanie skina zostało anulowane.");
                    return;
                }
            }

            int price = -1;
            while (price < 0)
            {
                InputWindow priceInputWindow = new InputWindow("Wpisz cenę skina:", "Cena skina");
                bool? priceResult = priceInputWindow.ShowDialog();
                if (priceResult == true)
                {
                    if (int.TryParse(priceInputWindow.UserInput, out price) && price >= 0)
                    {
                        // Cena jest poprawna
                    }
                    else
                    {
                        MessageBox.Show("Podano nieprawidłową cenę. Cena musi być liczbą całkowitą i nie może być ujemna.");
                        price = -1;
                    }
                }
                else
                {
                    MessageBox.Show("Dodawanie skina zostało anulowane.");
                    return;
                }
            }

            string side = null;
            while (string.IsNullOrEmpty(side))
            {
                InputWindow sideInputWindow = new InputWindow("Wpisz stronę (Terroryści, Anty-Terroryści lub Obie):", "Strona");
                bool? sideResult = sideInputWindow.ShowDialog();
                if (sideResult == true)
                {
                    side = sideInputWindow.UserInput;
                    if (string.IsNullOrEmpty(side))
                    {
                        MessageBox.Show("Strona nie może być pusta.");
                    }
                }
                else
                {
                    MessageBox.Show("Dodawanie skina zostało anulowane.");
                    return;
                }
            }

            string weaponCategory = null;
            while (string.IsNullOrEmpty(weaponCategory))
            {
                InputWindow weaponCategoryInputWindow = new InputWindow("Wpisz kategorię broni:", "Kategoria broni");
                bool? weaponCategoryResult = weaponCategoryInputWindow.ShowDialog();
                if (weaponCategoryResult == true)
                {
                    weaponCategory = weaponCategoryInputWindow.UserInput;
                    if (string.IsNullOrEmpty(weaponCategory))
                    {
                        MessageBox.Show("Kategoria broni nie może być pusta.");
                    }
                }
                else
                {
                    MessageBox.Show("Dodawanie skina zostało anulowane.");
                    return;
                }
            }

            _viewModel.AddNewSkin(name, collection, weaponType, price, side, weaponCategory);
            MessageBox.Show($"Dodano skina do repozytorium!");
        }


        private async void OnRemoveSkin(object sender, RoutedEventArgs e)
        {
            await _viewModel.DisplaySkinsAsync();

            var skins = _viewModel.Skins;

            if (!skins.Any())
            {
                MessageBox.Show("Brak skórek do usunięcia!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var skinChoices = skins.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            var dialog = new SelectSkinDialog("Wybierz skin do usunięcia:", skinChoices);
            if (dialog.ShowDialog() == true)
            {
                var selectedSkinData = dialog.SelectedSkin?.Split('(');
                if (selectedSkinData == null || selectedSkinData.Length < 2)
                {
                    MessageBox.Show("Niepoprawny wybór skina!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedSkinName = selectedSkinData[0].Trim();

                var skinToRemove = skins.FirstOrDefault(s => s.Name.Equals(selectedSkinName, StringComparison.OrdinalIgnoreCase));
                if (skinToRemove != null)
                {
                    _viewModel.RemoveSkin(skinToRemove);
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
            await _viewModel.DisplaySkinsAsync(); 
            var skins = _viewModel.Skins;

            if (!skins.Any())
            {
                MessageBox.Show("Brak skórek do dodania do ulubionych!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var skinChoices = skins.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            var dialog = new SelectSkinDialog("Wybierz skin do dodania do ulubionych:", skinChoices);
            if (dialog.ShowDialog() == true)
            {
                var selectedSkinData = dialog.SelectedSkin?.Split('(');
                if (selectedSkinData == null || selectedSkinData.Length < 2)
                {
                    MessageBox.Show("Niepoprawny wybór skina!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedSkinName = selectedSkinData[0].Trim();

                var skinToAdd = skins.FirstOrDefault(s => s.Name.Equals(selectedSkinName, StringComparison.OrdinalIgnoreCase));
                if (skinToAdd != null)
                {
                    _viewModel.AddSkinToFavorites(skinToAdd);
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
            await _viewModel.DisplayFavoriteSkinsAsync(); 

            var favoriteSkins = _viewModel.Skins.Where(s => s.IsFavorite).ToList();

            if (!favoriteSkins.Any())
            {
                MessageBox.Show("Brak ulubionych skórek do usunięcia!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var skinChoices = favoriteSkins.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            var dialog = new SelectSkinDialog("Wybierz skin do usunięcia z ulubionych:", skinChoices);
            if (dialog.ShowDialog() == true)
            {
                var selectedSkinData = dialog.SelectedSkin?.Split('(');
                if (selectedSkinData == null || selectedSkinData.Length < 2)
                {
                    MessageBox.Show("Niepoprawny wybór skina!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedSkinName = selectedSkinData[0].Trim();

                var skinToRemove = favoriteSkins.FirstOrDefault(s => s.Name.Equals(selectedSkinName, StringComparison.OrdinalIgnoreCase));
                if (skinToRemove != null)
                {
                    _viewModel.RemoveSkinFromFavorites(skinToRemove); 
                    MessageBox.Show($"Skin \"{skinToRemove.Name}\" został usunięty z ulubionych.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nie znaleziono wybranego skina do usunięcia z ulubionych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OnExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = (SolidColorBrush)FindResource("ButtonHoverColor");
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = (SolidColorBrush)FindResource("ButtonBackgroundColor");
            }
        }
    }
}
