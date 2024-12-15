namespace ApkaSkiny.Models
{
    public class SkinRepository
    {
        private List<Skin> skins;
        private List<Skin> favorites;

        public readonly Dictionary<string, (string WeaponCategory, string Side)> WeaponInfoMap = new Dictionary<string, (string, string)>
        {
            { "AK-47", ("Rifle", "Terrorists") },
            { "M4A4", ("Rifle", "Counter-Terrorists") },
            { "M4A1-S", ("Rifle", "Counter-Terrorists") },
            { "AWP", ("Sniper", "Obie") },
            { "Desert Eagle", ("Pistol", "Obie") },
            { "USP-S", ("Pistol", "Counter-Terrorists") },
            { "P250", ("Pistol", "Terrorists") },
            { "FAMAS", ("Rifle", "Counter-Terrorists") },
            { "AUG", ("Rifle", "Counter-Terrorists") },
            { "SG 553", ("Rifle", "Counter-Terrorists") },
            { "Galil AR", ("Rifle", "Terrorists") },
            { "Tec-9", ("Pistol", "Terrorists") },
            { "Karambit", ("Melee", "Obie") },
            { "Bayonet", ("Melee", "Obie") },
            { "Bowie Knife", ("Melee", "Obie") },
            { "P90", ("Submachine Gun", "Obie") },
            { "Mac-10", ("Submachine Gun", "Terrorists") },
            { "UMP-45", ("Submachine Gun", "Obie") },
            { "PP-Bizon", ("Submachine Gun", "Terrorists") },
            { "Nova", ("Shotgun", "Obie") },
            { "XM1014", ("Shotgun", "Obie") },
            { "MP9", ("Submachine Gun", "Counter-Terrorists") },
            { "MP7", ("Submachine Gun", "Counter-Terrorists") },
            { "Negev", ("Machine Gun", "Terrorists") },
            { "M249", ("Machine Gun", "Counter-Terrorists") },
            { "R8 Revolver", ("Revolver", "Terrorists") },
            { "Scar-20", ("Sniper", "Terrorists") }
        };

        public SkinRepository()
        {
            skins = new List<Skin>
            {
                new Skin("Dragon Lore", "Cobblestone", "AWP", 1500, GetWeaponInfo("AWP").Side, GetWeaponInfo("AWP").WeaponCategory),
                new Skin("Asiimov", "Phoenix", "AWP", 60, GetWeaponInfo("AWP").Side, GetWeaponInfo("AWP").WeaponCategory),
                new Skin("Redline", "Phoenix", "AK-47", 30, GetWeaponInfo("AK-47").Side, GetWeaponInfo("AK-47").WeaponCategory),
                new Skin("Hyper Beast", "Chroma 2", "M4A1-S", 25, GetWeaponInfo("M4A1-S").Side, GetWeaponInfo("M4A1-S").WeaponCategory),
                new Skin("Karambit", "Chroma 3", "Knife", 1000, GetWeaponInfo("Karambit").Side, GetWeaponInfo("Karambit").WeaponCategory),
                new Skin("Bowie Knife", "Chroma 3", "Knife", 900, GetWeaponInfo("Bowie Knife").Side, GetWeaponInfo("Bowie Knife").WeaponCategory)
            };
            favorites = new List<Skin>();
        }

        public (string WeaponCategory, string Side) GetWeaponInfo(string weaponName)
        {
            if (WeaponInfoMap.TryGetValue(weaponName, out var weaponInfo))
            {
                return weaponInfo;
            }

            if (weaponName.Contains("Knife"))
            {
                return ("Melee", "Obie");
            }

            return ("Unknown", "Unknown");
        }


        public List<Skin> GetSkins(string sortBy = null)
        {
            if (sortBy == "price")
            {
                return skins.OrderBy(s => s.Price).ToList();
            }
            if (sortBy == "collection")
            {
                return skins.OrderBy(s => s.Collection).ToList();
            }
            if (sortBy == "weaponType")
            {
                return skins.GroupBy(s => s.WeaponType)
                            .OrderBy(g => g.Key)
                            .SelectMany(g => g.OrderBy(s => s.Name))
                            .ToList();
            }
            return skins;
        }

        public List<Skin> GetSkinsByCollection(string collection)
        {
            return skins.Where(s => s.Collection == collection).ToList();
        }

        public List<Skin> GetSkinByType(string type)
        {
            return skins.Where(s => s.WeaponType == type).ToList();
        }

        public void AddToFavorites(Skin skin)
        {
            if (!favorites.Contains(skin))
            {
                favorites.Add(skin);
            }
        }

        public List<Skin> GetFavorites()
        {
            return favorites;
        }

        public void AddSkin(Skin skin)
        {
            skins.Add(skin);
        }

        public void RemoveSkinFromFavorites(Skin skin)
        {
            if (favorites.Contains(skin))
            {
                favorites.Remove(skin);
            }
        }

        public void RemoveSkin(Skin skin)
        {
            if (skins.Contains(skin))
            {
                skins.Remove(skin);
            }
            if (favorites.Contains(skin))
            {
                favorites.Remove(skin);
            }
        }

    }
}
