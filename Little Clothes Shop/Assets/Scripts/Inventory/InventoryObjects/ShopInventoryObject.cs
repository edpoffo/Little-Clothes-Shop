using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventoryObject : InventoryObject
{
    public override void OnClicked()
    {
        ShoppingUI.UI.AddToCart(Item);
    }
}
