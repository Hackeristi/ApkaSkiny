using ApkaSkiny.Models;

public class Skin
{
    public string Name { get; set; }
    public string Collection { get; set; }
    public string WeaponType { get; set; }
    public decimal Price { get; set; }
    public string Side { get; set; }
    public string WeaponCategory { get; set; }
    public bool IsFavorite { get; set; }

    public Skin(string name, string collection, string weaponType, decimal price, string side, string weaponCategory, bool isFavorite = false)
    {
        Name = name;
        Collection = collection;
        WeaponType = weaponType;
        Price = price;
        Side = side;
        WeaponCategory = weaponCategory;
        IsFavorite = false;
    }

    public override string ToString()
    {
        var repository = new SkinRepository();
        var weaponInfo = repository.GetWeaponInfo(WeaponType);

        return $"{Name} ({Collection}) - {WeaponType} ({weaponInfo.WeaponCategory}) : ${Price:F2} - Side: {weaponInfo.Side} - {(IsFavorite ? "★" : " ")}";
    }
}
