using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PersonalInventoryObject : InventoryObject
{
    public override void OnClicked()
    {
        if (ShoppingUI.UI.canBuy) Sell(Cashier.SellMultiplier);
    }

    private void Sell(float priceMultiplier)
    {
        CashUI.Cash.CashFlow(Item.Price * priceMultiplier);
        Player.P.inventory.Remove(Item);
    }

// TODO: Hover to show info
}
