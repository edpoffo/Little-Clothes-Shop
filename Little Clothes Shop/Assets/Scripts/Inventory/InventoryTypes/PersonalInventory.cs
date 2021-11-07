using System.Collections.Generic;
using System.Linq;

public class PersonalInventory : InventoryType
{
    public readonly int _bagSize = 30;
    protected override string PrefabPath => "Prefabs/InventoryObjects/PersonalInventoryObject";

    /// <summary>
    /// As this inventory has a limited size, this method tries to put the item inside, if the inventory is full, it will just return false
    /// TODO: If the item can stack, tries to find the stackable item first
    /// </summary>
    /// <param name="item">The item the player is trying to add</param>
    /// <returns>true if succeeded</returns>
    public bool TryAdd(InventoryItem item)
    {
        if (!HasFreeSlot()) return false;
        
        _items.Add(item);
        if(InventoryUI.UI) InventoryUI.UI.UpdateInventory();
        
        return true;
    }

    /// <summary>
    /// Removes an item and update the inventory display
    /// </summary>
    public void Remove(InventoryItem item)
    {
        _items.Remove(item);
        if (InventoryUI.UI) InventoryUI.UI.UpdateInventory();
    }

    /// <summary>
    /// Checks if the inventory has at least one free slot
    /// </summary>
    public bool HasFreeSlot()
    {
        return (_items.Count < _bagSize);
    }
    
    /// <summary>
    /// Returns all items with a specific itemType name
    /// </summary>
    public List<InventoryItem> Filter(string itemType)
    {
        return _items.Where(item => item.ItemType == itemType).ToList();
    }
}
