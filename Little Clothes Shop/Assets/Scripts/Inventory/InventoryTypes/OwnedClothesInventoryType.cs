using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OwnedClothesInventoryType : InventoryType
{
    protected override string PrefabPath => "Prefabs/InventoryObjects/StoredClothingInventoryObject";

    /// <summary>
    /// Removes previous objects and respawn the list of items
    /// </summary>
    /// <param name="spillArea">The parent GameObject</param>
    public override void UpdateItems(GameObject spillArea)
    {
        _items = Player.P.inventory.Filter("Clothing");
        base.UpdateItems(spillArea);
    }

    public void UpdateBodyTypeFilteredItems(GameObject spillArea, Person.BodyType bodyType)
    {
        var rawList = Player.P.inventory.Filter("Clothing");

        var newList = new List<InventoryItem>();
        foreach (var c in rawList)
        {
            var condA = c.Limitation == "";
            var condB = (c.Limitation == "Male" && Player.PlayerBodyType == Person.BodyType.Male);
            var condC = (c.Limitation == "Female" && Player.PlayerBodyType == Person.BodyType.Female);
            
            if(condA || condB || condC) newList.Add(c);
        }

        _items = newList;
        base.UpdateItems(spillArea);
    }
}
