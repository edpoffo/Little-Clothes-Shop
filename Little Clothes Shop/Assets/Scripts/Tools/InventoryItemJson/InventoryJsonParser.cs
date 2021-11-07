using UnityEngine;
using System.IO;
using Tools.InventoryItemJson;
using System.Collections.Generic;

public class InventoryJsonParser : MonoBehaviour
{
    /// <summary>
    /// Loads and returns a inventory at path
    /// </summary>
    /// <param name="path">The resources Path to the inventory</param>
    /// <param name="blankInventory">A blank inventory (InventoryType) compatible with the application</param>
    /// <returns></returns>
    public static InventoryType GetResourcesInventory(string path, InventoryType blankInventory)
    {
        var preInv = LoadResourceInventoryJson(path);
        blankInventory = UnpackInventory(preInv, blankInventory);

        return blankInventory;
    }

    #region Generate Json Files

    /// <summary>
    /// To Generate a JSON file, add the InventoryJsonParser component to a blank GameObject and change the "GenerateInventoryJson" Method with the intended Inventory
    /// </summary>
    private void Awake()
    {
        GenerateInventoryJson();
    }

    InventoryType inv = new ShoppingInventory();
    private void GenerateInventoryJson()
    {
        GenerateShirtShopInventory();
        GeneratePantsShopInventory();
        GenerateShoesInventory();
        GenerateAccessoriesInventory();
    }

    private void GenerateShirtShopInventory()
    {
        inv = new ShoppingInventory();
        
        // Male Long Sleeve
        AddMultiple("male long sleeved shirt",250, "Character/Clothes/MaleLongSleeveShirt", 32, "MaleLongSleeveShirt","Male");
        
        // Male sleeveless
        AddMultiple("male sleeveless shirt",200, "Character/Clothes/MaleSleevelessShirt", 32, "MaleSleevelessShirt","Male");
        
        // FormalCoat
        inv._items.Add(new ClothingItem("White formal coat shirt", 1250, "Character/Clothes/FormalCoat", 32, Color.white,"FormalCoat", ""));
        inv._items.Add(new ClothingItem("Black formal coat shirt", 1250, "Character/Clothes/FormalCoat", 32, new Color(.4f,.4f,.4f,1),"FormalCoat", ""));

        // FormalVest
        inv._items.Add(new ClothingItem("White formal vest", 600, "Character/Clothes/FormalVest", 32, Color.white,"FormalVest", ""));
        inv._items.Add(new ClothingItem("Black formal vest", 600, "Character/Clothes/FormalVest", 32, new Color(.4f,.4f,.4f,1),"FormalVest", ""));
        
        // Shirt
        AddMultiple("Male shirt",375, "Character/Clothes/MaleShirt", 32, "MaleShirt","");
        
        // Female Blouse
        AddMultiple("female blouse", 200, "Character/Clothes/FemaleBlouse", 32, "FemaleBlouse", "Female");
        
        // Female Corset
        AddMultiple("female corset", 450, "Character/Clothes/FemaleCorset", 32, "FemaleCorset", "Female");
        
        // Female Long sleeved blouse
        AddMultiple("female long sleeved blouse", 275, "Character/Clothes/FemaleLongsleevedBlouse", 32, "FemaleLongsleevedBlouse", "Female");

        // Female Long sleeved shirt
        AddMultiple("female long sleeved shirt", 200, "Character/Clothes/FemaleLongsleevedShirt", 32, "FemaleLongsleevedShirt", "Female");
        
        // Female Scoop
        AddMultiple("female scoop", 235, "Character/Clothes/FemaleScoop", 32, "FemaleScoop", "Female");
        
        // Female Sleeveless Shirt
        AddMultiple("female sleeveless shirt", 100, "Character/Clothes/FemaleSleevelessShirt", 32, "FemaleSleevelessShirt", "Female");
        
        // Female tight dress
        AddMultiple("female tight dress", 850, "Character/Clothes/FemaleTightDress", 32, "FemaleTightDress", "Female");
        
        // Save inventory
        SaveInventory(inv, "Shirts");
    }
    
    private void GeneratePantsShopInventory()
    {
        inv = new ShoppingInventory();
        
        // Pants
        AddMultiple("pants", 125, "Character/Clothes/Pants", 32, "Pants", null);
        
        // Belt
        inv._items.Add(new ClothingItem("Leather belt", 200, "Character/Clothes/LeatherBelt", 32, Color.white,"LeatherBelt", ""));
        inv._items.Add(new ClothingItem("Formal belt", 200, "Character/Clothes/FormalBelt", 32, Color.white,"FormalBelt", ""));
        
        // Save inventory
        SaveInventory(inv, "Pants");
    }

    private void GenerateShoesInventory()
    {
        inv = new ShoppingInventory();
        
        // Shoes
        AddMultiple("shoes", 125, "Character/Clothes/Shoes", 32, "Shoes", "");
        
        // Formal shoes
        inv._items.Add(new ClothingItem("Formal shoes", 200, "Character/Clothes/FormalShoes", 32, Color.white,"FormalShoes", ""));
        
        // Save inventory
        SaveInventory(inv, "Shoes");
    }
    
    private void GenerateAccessoriesInventory()
    {
        inv = new ShoppingInventory();
        
        // Bandana
        AddMultiple("bandana", 125, "Character/Clothes/Bandana", 32, "Bandana", "");
        
        // Bowtie
        AddMultiple("bowtie", 125, "Character/Clothes/Bowtie", 32, "Bowtie", "");
        
        // ChaplinHat
        inv._items.Add(new ClothingItem("Chaplin hat", 5000, "Character/Clothes/ChaplinHat", 32, Color.white,"ChaplinHat", ""));
        
        // Glasses
        AddMultiple("glasses", 125, "Character/Clothes/Glasses", 32, "Glasses", "");
        
        // Gloves
        AddMultiple("gloves", 125, "Character/Clothes/Gloves", 32, "Gloves", "");
        
        // Necktie
        inv._items.Add(new ClothingItem("Necktie", 200, "Character/Clothes/Necktie", 32, Color.white,"Necktie", ""));
        
        // Scarf
        AddMultiple("scarf", 125, "Character/Clothes/Scarf", 32, "Scarf", "");
        
        // Sunglasses
        inv._items.Add(new ClothingItem("Sunglasses", 200, "Character/Clothes/Sunglasses", 32, Color.white,"Sunglasses", ""));
        
        // Top hat
        inv._items.Add(new ClothingItem("Top hat", 200, "Character/Clothes/TopHat", 32, Color.white,"TopHat", ""));
        
        // Save inventory
        SaveInventory(inv, "Accessories");
    }
    
    private void AddMultiple(string itemName, float price, string path, int index, string spriteSheetName, string restriction)
    {
        // Male Long Sleeve
        inv._items.Add(new ClothingItem("White " + itemName, price, path, 32, Color.white,spriteSheetName, restriction));
        inv._items.Add(new ClothingItem("Black " + itemName, price, path, 32, new Color(.4f,.4f,.4f,1),spriteSheetName, restriction));
        inv._items.Add(new ClothingItem("Green " + itemName, price, path, 32, Color.green,spriteSheetName, restriction));
        inv._items.Add(new ClothingItem("Red "   + itemName, price, path, 32, Color.red,spriteSheetName, restriction));
        inv._items.Add(new ClothingItem("Blue "  + itemName, price, path, 32, Color.blue,spriteSheetName, restriction));
        inv._items.Add(new ClothingItem("'404' " + itemName, price, path, 32, Color.magenta,spriteSheetName, restriction));
        inv._items.Add(new ClothingItem("Pink "  + itemName, price, path, 32, new Color(1,.6f,.8f,1),spriteSheetName, restriction));
    }

    #endregion

    #region Save

    /// <summary>
    /// Saves the inventory into a JSON file
    /// </summary>
    /// <param name="inv">Inventory to be parsed</param>
    /// <param name="path">save path at /appdata/LocalLow/DefaultCompany/LittleClothesShop</param>
    private static void SaveInventory(InventoryType inv, string path)
    {
        if (inv == null) return;

        var i = PackInventory(inv);

        using (var stream = new StreamWriter(Application.persistentDataPath + "/" + path + ".json"))
        {
            var json = JsonUtility.ToJson(i);
            stream.Write(json);

            stream.Close();
        }
    }

    /// <summary>
    /// Transforms an inventory into a Json compatible format
    /// </summary>
    /// <param name="inv">Usable Inventory</param>
    /// <returns>JSON compatible format</returns>
    private static SerializableItemList PackInventory(InventoryType inv)
    {
        var siA = new List<SerializableItem>();
        foreach (var i in inv._items)
        {
            var sI = new SerializableItem();

            sI.itemName = i.ItemName;
            sI.itemValue = i.Price;

            var c =
                new float
                    [] // As Color cannot be serialized into a JSON, this breaks down the color in a array with 4 floats
                    {
                        i.ItemColor.r,
                        i.ItemColor.g,
                        i.ItemColor.b,
                        i.ItemColor.a
                    };
            sI.color = c;

            sI.itemSpritePath = i.iconPath;
            sI.itemSpriteIndex = i.iconIndex;
            sI.itemType = i.ItemType;
            sI.path = i.path;
            if (i.ItemType == "clothing")
            {
                var castedClothingItem = (ClothingItem) i;  // As different items could have multiple types of limitations
                castedClothingItem.Limitation = i.Limitation;
            }
            else
            {
                sI.limitType = i.Limitation;
                siA.Add(sI);
            }

            
        }

        var newItemList = new SerializableItemList();
        newItemList.itemArr = siA.ToArray();

        var siL = new SerializableItemList
        {
            itemArr = siA.ToArray()
        };

        return siL;
    }

    #endregion

    #region Load

    /// <summary>
    /// Loads a Json inventory from the resources path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static SerializableItemList LoadResourceInventoryJson(string path)
    {
        var json = Resources.Load<TextAsset>(path);
        if(!json) Debug.Log("JSON not found at path " + path); 
        var inv = JsonUtility.FromJson<SerializableItemList>(json.text);

        return inv;
    }

    /// <summary>
    /// Loads a JSON inventory at /appdata/LocalLow/DefaultCompany/LittleClothesShop/'path'
    /// </summary>
    /// <param name="path"> the path at to find the JSON at /appdata/LocalLow/DefaultCompany/LittleClothesShop</param>
    /// <returns> returns the JSON compatible inventory</returns>
    private SerializableItemList LoadInventory(string path)
    {
        using (var stream = new StreamReader(Application.persistentDataPath + "/" + path + ".json"))
        {
            var json = stream.ReadToEnd();
            var inv = JsonUtility.FromJson<SerializableItemList>(json);

            stream.Close();

            return inv;
        }
    }

    /// <summary>
    /// Transforms a JSON compatible format inventory into a Usable inventory
    /// </summary>
    /// <param name="siL">Inventory in JSON compatible format (post stream)</param>
    /// <param name="inv">blank inventory to be filled and returned</param>
    /// <returns>Inventory</returns>
    private static InventoryType UnpackInventory(SerializableItemList siL, InventoryType inv)
    {
        var itemList = new List<InventoryItem>();

        foreach (var i in siL.itemArr)
        {
            InventoryItem item = null;

            var c = new Color(i.color[0], i.color[1], i.color[2], i.color[3]);

            switch (i.itemType)
            {
                case "Clothing":
                    item = new ClothingItem(i.itemName, i.itemValue, i.itemSpritePath, i.itemSpriteIndex, c,i.path,i.limitType);
                    break;
                default:
                    Debug.Log("type " + i.itemType + " not found");
                    break;
            }

            if (item != null) itemList.Add(item);
        }

        inv._items = itemList;

        return inv;
    }

    /// <summary>
    /// Get the body type from a string to create a clothing item
    /// </summary>
    private static Person.BodyType? GetBodyTypeFromString(string bodyTypeString)
    {
        switch (bodyTypeString)
        {
            case "Male":
                return Person.BodyType.Male;
            case "Female":
                return Person.BodyType.Female;
            case "":
                return null;
            default:
                Debug.LogError("No body type registered with the " + bodyTypeString + "string");
                break;
        }
        return null;
    }

    /// <summary>
    /// Get the string from a body type to save it into a JSON
    /// </summary>
    private static string GetStringFromBodyType(Person.BodyType? bodyType)
    {
        switch (bodyType)
        {
            case Person.BodyType.Male:
                return "Male";
            case Person.BodyType.Female:
                return "Female";
            case null:
                return null;
            default:
                Debug.LogError("No string registered with the " + bodyType);
                break;
        }
        return null;
    }

    #endregion
}