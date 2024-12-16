using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ApkaSkiny.Models;
using System.ComponentModel;

namespace ApkaSkiny.ViewModels
{
    public class SkinViewModel : INotifyPropertyChanged
    {
        private SkinRepository _repository;
        private ObservableCollection<Skin> _skins;
        private ObservableCollection<Skin> _favoriteSkins;

        public ObservableCollection<Skin> Skins
        {
            get => _skins;
            set
            {
                _skins = value;
                OnPropertyChanged(nameof(Skins));
            }
        }

        public ObservableCollection<Skin> FavoriteSkins
        {
            get => _favoriteSkins;
            set
            {
                _favoriteSkins = value;
                OnPropertyChanged(nameof(FavoriteSkins));
            }
        }

        public SkinViewModel()
        {
            _repository = new SkinRepository();
            Skins = new ObservableCollection<Skin>();
            FavoriteSkins = new ObservableCollection<Skin>();
        }

        // Wyświetlanie wszystkich skinów
        public async Task DisplaySkinsAsync()
        {
            Skins.Clear();
            var skins = await Task.Run(() => _repository.GetSkins());
            foreach (var skin in skins)
            {
                Skins.Add(skin);
            }
        }

        // Wyświetlanie ulubionych skinów
        public async Task DisplayFavoriteSkinsAsync()
        {
            FavoriteSkins.Clear();
            var favorites = await Task.Run(() => _repository.GetFavorites());
            foreach (var skin in favorites)
            {
                FavoriteSkins.Add(skin);
            }
        }

        // Add skin to favorites
        public void AddSkinToFavorites(Skin skin)
        {
            if (!skin.IsFavorite)
            {
                skin.IsFavorite = true;
                _repository.AddToFavorites(skin);
                OnPropertyChanged(nameof(FavoriteSkins));
            }
        }

        // Remove skin from favorites
        public void RemoveSkinFromFavorites(Skin skin)
        {
            if (skin.IsFavorite)
            {
                skin.IsFavorite = false;
                _repository.RemoveSkinFromFavorites(skin);
                OnPropertyChanged(nameof(FavoriteSkins));
            }
        }

        // Add a new skin
        public void AddNewSkin(Skin skin)
        {
            var existingSkin = _repository.GetSkins().FirstOrDefault(s => s.Name.Equals(skin.Name, StringComparison.OrdinalIgnoreCase));

            if (existingSkin == null)
            {
                _repository.AddSkin(skin);
                Skins.Add(skin);
            }
            else
            {
                // Handle skin already exists scenario
            }
        }
        public async Task DisplaySkinsSortedByPriceAsync()
        {
            Skins.Clear();
            var skins = await Task.Run(() => _repository.GetSkins("price"));
            foreach (var skin in skins.OrderBy(s => s.Price))
            {
                Skins.Add(skin);
            }
        }
        public async Task DisplaySkinsByCollectionAsync(string collection)
        {
            Skins.Clear();
            var skins = await Task.Run(() => _repository.GetSkinsByCollection(collection));
            foreach (var skin in skins)
            {
                Skins.Add(skin);
            }
        }
        public async Task DisplaySkinsByWeaponTypeAsync(string weaponType)
        {
            Skins.Clear();
            var skins = await Task.Run(() => _repository.GetSkinByType(weaponType));
            foreach (var skin in skins)
            {
                Skins.Add(skin);
            }
        }
        public async Task DisplaySkinsByChosenPriceAsync(decimal? minPrice, decimal? maxPrice)
        {
            Skins.Clear();
            var skins = await Task.Run(() => _repository.GetSkins());
            var filteredSkins = skins.Where(s =>
                (!minPrice.HasValue || s.Price >= minPrice) &&
                (!maxPrice.HasValue || s.Price <= maxPrice)).ToList();

            foreach (var skin in filteredSkins)
            {
                Skins.Add(skin);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
