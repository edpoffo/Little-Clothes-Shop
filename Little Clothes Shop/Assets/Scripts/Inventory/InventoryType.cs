using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InventoryType
{
    #region Inventory prefab handling

    protected GameObject InventoryObjectPrefab => GetInvObjPrefab(); // This property will try to find the prefab and cache the prefab
    protected abstract string PrefabPath { get; }                  // The path to find the prefab
    private GameObject _cachedInvObjPrefab;                        // The found prefab is stored here for optimization purposes

    /// <summary>
    /// returns the prefab, if didn't found a prefab before, tries to find it first
    /// </summary>
    private GameObject GetInvObjPrefab()
    {
        if (!_cachedInvObjPrefab) _cachedInvObjPrefab = Resources.Load<GameObject>(PrefabPath);

        return _cachedInvObjPrefab;
    }

    #endregion

    // Lists
    public List<InventoryItem> _items = new List<InventoryItem>();           // All the Items in this inventory
    private List<GameObject> _itemInventoryObjects = new List<GameObject>(); // The displayed Inventory Objects

    /// <summary>
    /// Generates a game object for each object inside
    /// </summary>
    /// <param name="spillArea">The parent GameObject</param>
    /// <returns>The list of objects created</returns>
    protected virtual List<GameObject> SpillObjects(GameObject spillArea)
    {
        var objList = new List<GameObject>(); // This will be the list that will be filled and returned

        foreach (var i in _items)
        {
            var item = Object.Instantiate(InventoryObjectPrefab, spillArea.transform, true)
                .GetComponent<InventoryObject>();
            item.transform.localPosition = Vector3.zero;

            item.Item = i;
            item.Setup();

            objList.Add(item.gameObject);
        }

        return objList;
    }

    /// <summary>
    /// Removes previous objects and respawn the list of items
    /// </summary>
    /// <param name="spillArea">The parent GameObject</param>
    public virtual void UpdateItems(GameObject spillArea)
    {
        DestroyAllObjects();
        _itemInventoryObjects = SpillObjects(spillArea);
    }

    /// <summary>
    /// Destroy all inventory cached GameObjects
    /// </summary>
    public void DestroyAllObjects()
    {
        foreach (var itemObj in _itemInventoryObjects)
        {
            Object.Destroy(itemObj);
        }
    }

    /// <summary>
    /// Returns the sum of all values but scaling the value for a total discount
    /// </summary>
    /// <param name="multiplier">How much the value will be MULTIPLIED (optional)</param>
    /// <returns></returns>
    public float GetTotalValue(float multiplier = 1f)
    {
        return _items.Sum(item => item.Price * multiplier);
    }
}