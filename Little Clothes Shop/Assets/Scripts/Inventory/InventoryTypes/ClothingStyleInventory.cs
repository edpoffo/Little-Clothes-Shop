using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingStyleInventory : InventoryType
{
    protected override string PrefabPath => "Prefabs/InventoryObjects/ClothingStyleInventoryObject";

    private List<Clothes> _clothesList = new List<Clothes>();

    /// <summary>
    /// Generates a game object for each object inside
    /// </summary>
    /// <param name="spillArea">The parent GameObject</param>
    /// <returns>The list of objects created</returns>
    protected override List<GameObject> SpillObjects(GameObject spillArea)
    {
        var objList = new List<GameObject>(); // This will be the list that will be filled and returned

        foreach (var clothing in Player.P.currentClothes)
        {
            var invObj = Object.Instantiate(InventoryObjectPrefab, spillArea.transform, true)
                .GetComponent<StyleInventoryObject>();
            invObj.transform.localPosition = Vector3.zero;

            invObj.clothing = clothing;
            
            invObj.Item = clothing._clothingItem;
            invObj.Setup();

            objList.Add(invObj.gameObject);
        }

        return objList;
    }

    public override void UpdateItems(GameObject spillArea)
    {
        //_clothesList = Player.P.currentClothes;
        base.UpdateItems(spillArea);
    }
}
