using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopArea : MonoBehaviour
{
    [SerializeField] private string inventoryPath;
    [SerializeField] private string shopName;

    private InventoryType _inventoryType = new ShoppingInventory();

    // Start is called before the first frame update
    private void Start()
    {
        LoadInventoryFromResources();
    }

    /// <summary>
    /// Uses the JsonParser to find and load the Inventory
    /// </summary>
    private void LoadInventoryFromResources()
    {
        _inventoryType = InventoryJsonParser.GetResourcesInventory("JSON/ShopAreas/" + inventoryPath, _inventoryType);
    }

    public InventoryType GetShopInventory()
    {
        return _inventoryType;
    }

    private void OpenShop(object source, EventArgs args)
    {
        ShoppingUI.UI.OpenShoppingArea(_inventoryType, shopName);
    }

    #region Triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) other.GetComponent<Player>().Used += OpenShop;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) other.GetComponent<Player>().Used -= OpenShop;
    }

    #endregion
}