using ApkaSkiny.Models;
using Spectre.Console;
using Figgle;
using global::ApkaSkiny.Views;

    namespace ApkaSkiny
    {
        public class Controller
        {
            private SkinRepository _repository;
            private readonly IUI _view;

        public Controller(IUI view)
        {
            _repository = new SkinRepository();
            _view = view;
        }

        public void Run()
            {
                _view.ShowTitle("Przeglądarka skinów CS2");
                ShowAsciiAnimation();

                bool isRunning = true;

                while (isRunning)
                {
                    var choice = _view.GetSelection("[#FFD6E9]Wybierz opcję:[/]", new List<string>

                {
                    "1. Wyświetl wszystkie skiny",
                    "2. Wyświetl ulubione skiny",
                    "3. Dodaj nowy skin",
                    "4. Usuń skin",
                    "5. Dodaj skin do ulubionych",
                    "6. Usuń skin z ulubionych",
                    "7. Sortuj po cenie",
                    "8. Sortuj po kolekcji",
                    "9. Sortuj po typie broni",
                    "10. Sortuj po wybranej cenie",
                    "11. Wyszukaj skin po nazwie",
                    "12. Statystyki",
                    "13. Wyjdź z aplikacji"
                });

                    switch (choice)
                    {
                        case "1. Wyświetl wszystkie skiny":
                            DisplaySkins();
                            break;

                        case "2. Wyświetl ulubione skiny":
                            DisplayFavoriteSkins();
                            break;

                        case "3. Dodaj nowy skin":
                            AddNewSkin();
                            break;

                        case "4. Usuń skin":
                            RemoveSkin();
                            break;

                        case "5. Dodaj skin do ulubionych":
                            AddSkinToFavorites();
                            break;

                        case "6. Usuń skin z ulubionych":
                            RemoveSkinFromFavorites();
                            break;

                        case "7. Sortuj po cenie":
                            DisplaySkinsSortedByPrice();
                            break;

                        case "8. Sortuj po kolekcji":
                            DisplaySkinsByCollection();
                            break;

                        case "9. Sortuj po typie broni":
                            DisplaySkinsByWeaponType();
                            break;

                        case "10. Sortuj po wybranej cenie":
                            DisplaySkinsByChosenPrice();
                            break;

                        case "11. Wyszukaj skin po nazwie":
                            SearchSkinsByName();
                            break;

                        case "12. Statystyki":
                            DisplayStatistics();
                            break;

                        case "13. Wyjdź z aplikacji":
                            _view.ShowMessage("[#0671B7]Zamykanie aplikacji...[/]");
                            isRunning = false;
                            break;

                        default:
                            _view.ShowMessage("[#0671B7]Nieprawidłowy wybór, spróbuj ponownie![/]");
                            break;
                    }
                }
            }

        public void ShowAsciiAnimation()
            {
                string[] animationFrames = new string[] { "ŁADOWANIE", "ŁADOWANIE.", "ŁADOWANIE..", "ŁADOWANIE..." };
                _view.ShowAsciiAnimation(animationFrames);
            }

        public void PrintSkinsTable(IEnumerable<Skin> skins)
            {
                AnsiConsole.Clear();
                _view.PrintSkinsTable(skins);
            }

        public void DisplaySkins()
            {
                AnsiConsole.Clear();
                _view.ShowMessage("[#FFD6E9]Ładowanie wszystkich skinów...[/]");
                var skins = _repository.GetSkins();
                if (!skins.Any())
                {
                    _view.ShowMessage("[#D30E92]Brak danych do wyświetlenia.[/]");
                    return;
                }
                PrintSkinsTable(skins);
            }

        public void DisplaySkinsSortedByPrice()
            {
                AnsiConsole.Clear();
                _view.ShowMessage("[#FFD6E9]Sortowanie skinów po cenie...[/]");
                var skins = _repository.GetSkins("price");
                PrintSkinsTable(skins);
            }

        public void DisplaySkinsByCollection()
            {
                AnsiConsole.Clear();
                var collections = _repository.GetSkins().Select(s => s.Collection).Distinct().ToList();
                var collectionChoice = _view.GetSelection("Wybierz kolekcję:", collections);
                var skins = _repository.GetSkinsByCollection(collectionChoice);
                PrintSkinsTable(skins);
            }

        public void DisplaySkinsByWeaponType()
            {
                AnsiConsole.Clear();
                var weaponTypes = _repository.GetSkins().Select(s => s.WeaponType).Distinct().ToList();
                var weaponTypeChoice = _view.GetSelection("Wybierz typ broni:", weaponTypes);
                var skins = _repository.GetSkinByType(weaponTypeChoice);
                PrintSkinsTable(skins);
            }

        public void DisplaySkinsByChosenPrice()
            {
                var allSkins = _repository.GetSkins();

                var minPriceChoice = _view.GetUserInput("Podaj minimalną cenę (pozostaw puste, aby nie filtrować):");
                decimal? minPrice = string.IsNullOrEmpty(minPriceChoice) ? (decimal?)null : decimal.Parse(minPriceChoice);

                var maxPriceChoice = _view.GetUserInput("Podaj maksymalną cenę (pozostaw puste, aby nie filtrować):");
                decimal? maxPrice = string.IsNullOrEmpty(maxPriceChoice) ? (decimal?)null : decimal.Parse(maxPriceChoice);

                var filteredSkins = allSkins
                .Where(s =>
                {
                    bool withinMinPrice = !minPrice.HasValue || s.Price >= minPrice.Value;
                    bool withinMaxPrice = !maxPrice.HasValue || s.Price <= maxPrice.Value;
                    return withinMinPrice && withinMaxPrice;
                })
                .OrderBy(s => minPrice.HasValue ? s.Price : (decimal?)null)
                .ThenByDescending(s => !minPrice.HasValue ? s.Price : (decimal?)null)
                .ToList();

                if (filteredSkins.Any())
                {
                    PrintSkinsTable(filteredSkins);
                }
                else
                {
                    _view.ShowMessage("[#D30E92]Brak skinów w wybranym zakresie cenowym![/]");
                }
            }


        public void DisplayFavoriteSkins()
            {
                AnsiConsole.Clear();
                _view.ShowMessage("[#FFD6E9]Wyświetlanie ulubionych skinów...[/]");
                var favorites = _repository.GetFavorites();
                if (favorites.Any())
                {
                    PrintSkinsTable(favorites);
                }
                else
                {
                    _view.ShowMessage("[#D30E92]Brak ulubionych skinów. Dodaj ulubiony skin![/]");
                }
            }

        public void AddNewSkin()
        {
            AnsiConsole.Clear();
            _view.ShowMessage("[#FFD6E9]Dodawanie nowego skina...[/]");

            string name;
            do
            {
                name = _view.GetUserInput("Podaj nazwę skina:");
                name = CapitalizeFirstLetter(name).Trim();

                if (string.IsNullOrEmpty(name))
                {
                    _view.ShowMessage("[#D30E92]Nazwa skina nie może być pusta![/]");
                }
            } while (string.IsNullOrEmpty(name));

            string collection;
            do
            {
                collection = _view.GetUserInput("Podaj nazwę kolekcji:");
                collection = CapitalizeFirstLetter(collection).Trim();

                if (string.IsNullOrEmpty(collection))
                {
                    _view.ShowMessage("[#D30E92]Nazwa kolekcji nie może być pusta![/]");
                }
            } while (string.IsNullOrEmpty(collection));

            var availableWeaponTypes = _repository.WeaponInfoMap.Keys.ToList();
            var weaponType = GetValidInputFromUser("typ broni", availableWeaponTypes);

            var availableWeaponCategories = new HashSet<string>(_repository.WeaponInfoMap.Values.Select(w => w.WeaponCategory));
            var weaponCategory = GetValidInputFromUser("kategorię broni", availableWeaponCategories.ToList());

            decimal price = GetValidPriceFromUser();

            var existingSkin = _repository.GetSkins()
                                          .FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                                               s.Collection.Equals(collection, StringComparison.OrdinalIgnoreCase) &&
                                                               s.WeaponType.Equals(weaponType, StringComparison.OrdinalIgnoreCase) &&
                                                               s.WeaponCategory.Equals(weaponCategory, StringComparison.OrdinalIgnoreCase));

            if (existingSkin != null)
            {
                _view.ShowMessage("[#0671B7]Nie dodano skina. Podany skin jest już w bazie.[/]");
                return;
            }

            string side = string.Empty;
            int attemptCount = 0;
            while (attemptCount < 3)
            {
                side = _view.GetSelection("Podaj stronę:", new List<string> { "Terrorists", "Counter-Terrorists", "Obie" });

                if (IsValidSideForWeaponType(weaponType, side))
                {
                    break;
                }

                _view.ShowMessage($"[#D30E92]Strona \"{side}\" nie pasuje do typu broni \"{weaponType}\". Spróbuj ponownie![/]");
                attemptCount++;
            }

            if (attemptCount == 3)
            {
                side = AssignDefaultSideForWeaponType(weaponType);
                _view.ShowMessage($"[#FFC0EE]Przypisano domyślną stronę \"{side}\" dla typu broni \"{weaponType}\". Naciśnij Enter, aby kontynuować.[/]");
                WaitForUserToContinue();
            }

            var newSkin = new Skin(name, collection, weaponType, price, side, weaponCategory);

            _repository.AddSkin(newSkin);
            AnsiConsole.Clear();
            _view.ShowMessage($"[#FFD6E9]Dodano nowy skin: {name} w cenie ${price:F2}.[/]");
        }


        public void WaitForUserToContinue()
        {
            AnsiConsole.MarkupLine("[grey][[INFO]] Naciśnij Enter, aby kontynuować...[/]");
            Console.ReadLine();
        }

        public bool IsValidSideForWeaponType(string weaponType, string side)
        {
            return _repository.WeaponInfoMap.TryGetValue(weaponType, out var weaponInfo) &&
                   (weaponInfo.Side == side || weaponInfo.Side == "Obie");
        }

        public string AssignDefaultSideForWeaponType(string weaponType)
        {
            if (_repository.WeaponInfoMap.TryGetValue(weaponType, out var weaponInfo))
            {
                return weaponInfo.Side;
            }
            return "Obie";
        }

        public decimal GetValidPriceFromUser()
        {
            decimal price = 1;
            bool isValidPrice = false;

            while (!isValidPrice)
            {
                var priceInput = _view.GetUserInput("Podaj cenę skina (w USD):");

                isValidPrice = decimal.TryParse(priceInput, out price) && price > 0;

                if (!isValidPrice)
                {
                    _view.ShowMessage("[#D30E92]Nieprawidłowa cena. Spróbuj ponownie.[/]");
                }
            }
            return price;
        }


        public string GetValidInputFromUser(string inputType, List<string> validOptions)
        {
            int attemptCount = 0;
            string userInput = string.Empty;

            while (attemptCount < 3)
            {
                userInput = CapitalizeFirstLetter(_view.GetUserInput($"Podaj {inputType}:"));

                if (validOptions.Contains(userInput, StringComparer.OrdinalIgnoreCase))
                {
                    break;
                }
                if (attemptCount >= 0 && attemptCount < 2)
                {
                    _view.ShowMessage($"[#D30E92]Nieprawidłowy {inputType}! Spróbuj ponownie.[/]");
                }

                attemptCount++;

                if (attemptCount == 3)
                {
                    _view.ShowMessage($"[#D30E92]Przekroczono limit prób! Oto dostępne {inputType}: [/]");
                    var selection = _view.GetSelection($"Wybierz jeden z dostępnych {inputType}:", validOptions);
                    userInput = selection;
                }
            }
            return userInput;
        }



        public string MatchWeaponType(string input, List<string> availableWeaponTypes)
        {
            var match = availableWeaponTypes.FirstOrDefault(wt =>
                string.Equals(wt, input, StringComparison.OrdinalIgnoreCase));

            return match ?? CapitalizeFirstLetter(input); 
        }

        public string CapitalizeFirstLetter(string input)
            {
                return string.IsNullOrWhiteSpace(input) ? input : char.ToUpper(input[0]) + input.Substring(1).ToLower();
            }

        public void AddSkinToFavorites()
        {
            AnsiConsole.Clear();
            var skins = _repository.GetSkins();

            var skinChoices = skins.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            var skinChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz skin do dodania do ulubionych:")
                    .AddChoices(skinChoices)
            );

            var selectedSkinName = skinChoice.Split('(')[0].Trim();
            var skin = skins.FirstOrDefault(s => s.Name == selectedSkinName);

            if (skin != null)
            {
                skin.IsFavorite = true;

                _repository.AddToFavorites(skin);

                AnsiConsole.MarkupLine($"[#FFD6E9]Skin {skin.Name} został dodany do ulubionych.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[#D30E92]Nie znaleziono wybranego skina![/]");
            }
        }

        public void SearchSkinsByName()
        {
            AnsiConsole.Clear();
            var query = AnsiConsole.Ask<string>("Wprowadź fragment nazwy skina:");
            var results = _repository.GetSkins().Where(s => s.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Any())
            {
                PrintSkinsTable(results);
            }
            else
            {
                AnsiConsole.MarkupLine("[#D30E92]Nie znaleziono skinów pasujących do zapytania![/]");
            }
        }

        public void DisplayStatistics()
        {
            AnsiConsole.Clear();
            var skins = _repository.GetSkins();
            if (!skins.Any())
            {
                AnsiConsole.MarkupLine("[#D30E92]Brak danych do wyświetlenia statystyk.[/]");
                return;
            }

            var avgPrice = skins.Average(s => s.Price);
            var minPriceSkin = skins.OrderBy(s => s.Price).First();
            var maxPriceSkin = skins.OrderByDescending(s => s.Price).First();

            var table = new Table()
                .AddColumn("[#946656]Statystyka[/]")
                .AddColumn("[#946656]Wartość[/]")
                .AddRow("[#946656]Średnia cena[/]", $"[#DB76BC]${avgPrice:F2}[/]")
                .AddRow("[#946656]Najtańszy skin[/]", $"[#C79274]{minPriceSkin.Name}[/] - [#FFC0EE]${minPriceSkin.Price:F2}[/]")
                .AddRow("[#946656]Najdroższy skin[/]", $"[#C79274]{maxPriceSkin.Name}[/] - [#D30E92]${maxPriceSkin.Price:F2}[/]");

            AnsiConsole.Write(table);
        }
        public void RemoveSkin()
        {
            AnsiConsole.Clear();
            var skins = _repository.GetSkins();

            if (!skins.Any())
            {
                AnsiConsole.MarkupLine("[#D30E92]Brak skór do usunięcia![/]");
                return;
            }

            var skinChoices = skins.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            var skinChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz skin do usunięcia:")
                    .AddChoices(skinChoices)
            );

            var selectedSkinName = skinChoice.Split('(')[0].Trim();
            var skinToRemove = skins.FirstOrDefault(s => s.Name == selectedSkinName);

            if (skinToRemove != null)
            {
                _repository.RemoveSkin(skinToRemove);
                AnsiConsole.MarkupLine($"[#FFD6E9]Skin {skinToRemove.Name} został usunięty.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[#D30E92]Nie znaleziono wybranego skina do usunięcia.[/]");
            }
        }

        public void RemoveSkinFromFavorites()
        {
            AnsiConsole.Clear();
            var favorites = _repository.GetFavorites();

            if (!favorites.Any())
            {
                AnsiConsole.MarkupLine("[#D30E92]Brak ulubionych skinów do usunięcia![/]");
                return;
            }

            var skinChoices = favorites.Select(s => $"{s.Name} ({s.WeaponType})").ToList();

            var skinChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz skin do usunięcia z ulubionych:")
                    .AddChoices(skinChoices)
            );

            var selectedSkinName = skinChoice.Split('(')[0].Trim();
            var skinToRemoveFromFavorites = favorites.FirstOrDefault(s => s.Name == selectedSkinName);

            if (skinToRemoveFromFavorites != null)
            {
                skinToRemoveFromFavorites.IsFavorite = false;
                _repository.RemoveSkinFromFavorites(skinToRemoveFromFavorites);
                AnsiConsole.MarkupLine($"[#FFD6E9]Skin {skinToRemoveFromFavorites.Name} został usunięty z ulubionych.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[#D30E92]Nie znaleziono wybranego skina w ulubionych.[/]");
            }
        }
    }
}
