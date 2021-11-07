using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChangeClothesUI : MonoBehaviour
{
    public static ChangeClothesUI UI;
    
    // Limits
    public readonly int MINLayer = 1;
    public readonly int MAXLayer = 9;

    // Spill areas
    public GameObject ownedSpillArea;
    public GameObject styleSpillArea;
    
    // Inventories
    private OwnedClothesInventoryType ownedInventory = new OwnedClothesInventoryType();
    private ClothingStyleInventory styleInventory = new ClothingStyleInventory();
    
    private void Awake()
    {
        UI = this;
    }

    public void UpdateInventories()
    {
        // Fill owned inventory
        ownedInventory.UpdateBodyTypeFilteredItems(ownedSpillArea, Player.PlayerBodyType);
        // TODO: Fill clothing Style
        styleInventory.UpdateItems(styleSpillArea);
    }

    #region Button Handling

    public void OnWearClick()
    {
        if (!StoredClothingInventoryObject.SelectedStoredClothing) return;
        
        StoredClothingInventoryObject.SelectedStoredClothing.Wear();
        StoredClothingInventoryObject.SelectedStoredClothing = null;
    }

    public void OnRemoveClick()
    {
        if (!StyleInventoryObject.SelectedStyle) return;
        
        StyleInventoryObject.SelectedStyle.RemoveClothing();
        StyleInventoryObject.SelectedStyle = null;
    }

    #endregion

    #region Enabled/Disabled

    private void OnEnable()
    {
        UpdateInventories();
        
    }

    private void OnDisable()
    {
        StyleInventoryObject.SelectedStyle = null;
        StoredClothingInventoryObject.SelectedStoredClothing = null;
    }

    #endregion
}
