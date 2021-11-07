using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ClothingItem : InventoryItem
{
    // private const string ClothingResourcesPath = "Character/Clothes/";
    public override string ItemName => clothingName;
    [SerializeField] private string clothingName;
    public override float Price => clothingPrice;
    [SerializeField] private float clothingPrice;
    public override string ItemType => "Clothing";
    public Person.BodyType? BodyLimitation;

    public override string Limitation
    {
        get => CachedLimitation;
        set => CachedLimitation = SetLimitation(value);
    }

    private string SetLimitation(string value)
    {
        switch (value)
        {
            case "Male":
                BodyLimitation = Person.BodyType.Male;
                break;
            case "Female":
                BodyLimitation = Person.BodyType.Female;
                break;
            case "":
                BodyLimitation = null;
                break;
        }

        return value;
    }

    #region Constructors

    /// <summary>
    /// If the spritesheet has only one Sprite, use this method
    /// </summary>
    /// <param name="itemName">The displayed name</param>
    /// <param name="itemPrice">The cost for the item</param>
    /// <param name="spritePath">The path on the resources folder for the sprite sheet</param>
    /// <param name="iColor">The color modifier</param>
    /// <param name="spriteSheetName">The name of the spritesheet</param>
    /// <param name="bodyType">String for the body type</param>
    public ClothingItem(string itemName, float itemPrice, string spritePath, Color iColor, string spriteSheetName, string bodyType)
    {
        clothingName = itemName;
        clothingPrice = itemPrice;
        CachedIcon = Resources.LoadAll<Sprite>(spritePath)[0];
        ItemColor = iColor;
        iconPath = spritePath;
        iconIndex = -1;
        path = spriteSheetName;
        Limitation = bodyType;
    }

    /// <summary>
    /// If the spritesheet has more than one Sprite, use this method
    /// </summary>
    /// <param name="itemName">The displayed name</param>
    /// <param name="itemPrice">The cost for the item</param>
    /// <param name="spritePath">The path on the resources folder for the sprite sheet</param>
    /// <param name="spriteIndex">The index on the spriteSheet</param>
    /// <param name="iColor">The color modifier</param>
    /// <param name="spriteSheetName">The name of the spritesheet</param>
    public ClothingItem(string itemName, float itemPrice, string spritePath, int spriteIndex, Color iColor, string spriteSheetName, string bodyType)
    {
        clothingName = itemName;
        clothingPrice = itemPrice; CachedIcon = Resources.LoadAll<Sprite>(spritePath)[spriteIndex];
        ItemColor = iColor;
        iconPath = spritePath;
        iconIndex = spriteIndex;
        path = spriteSheetName;
        Limitation = bodyType;
    }

    #endregion

    public override void ExtraUse()
    {
        Wear();
    }

    #region Clothing specific methods

    private void Wear()
    {
        Player.P.Wear(this, 1);
        Player.P.inventory.Remove(this);
    }

    #endregion
}