using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCartInventoryObject : InventoryObject
{
    public override void OnClicked()
    {
        ShoppingUI.UI.RemoveFromCart(Item);
    }
}
