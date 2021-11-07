using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class InventoryObject : MonoBehaviour
{
    public InventoryItem Item;

    // Components
    [SerializeField] private Text nameText;
    [SerializeField] private Text typeText;
    [SerializeField] private Text priceText;
    [SerializeField] private Image IconImage;

    public void Setup()
    {
        // Change the displayed features (as the inventory object can assume different layouts, the "if" statements will allow for this flexibility)  
        if (nameText) nameText.text = Item.ItemName;
        if (typeText) typeText.text = Item.ItemType;
        if (priceText) priceText.text = Item.Price.ToString("n2");
        if (IconImage)
        {
            IconImage.sprite = Item.Icon;
            IconImage.color = Item.ItemColor;
        }
    }

    public abstract void OnClicked();
}