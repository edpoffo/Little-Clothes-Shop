using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cashier : MonoBehaviour
{
    public const float SellMultiplier = .4f;
    private void InteractCashier(object source, EventArgs args)
    {
        ShoppingUI.UI.OnShoppingCartOpenButtonClick();
    }

    #region Triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShoppingUI.UI.canBuy = true;
            other.GetComponent<Player>().Used += InteractCashier;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShoppingUI.UI.canBuy = false;
            other.GetComponent<Player>().Used -= InteractCashier;
        }
    }

    #endregion
}
