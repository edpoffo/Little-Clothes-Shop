using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShoppingUI : MonoBehaviour
{
    public static ShoppingUI UI;

    // Components
    [SerializeField] private GameObject shoppingCartButton;
    [SerializeField] private GameObject shoppingWindow;
    [SerializeField] private GameObject shoppingWindowSpillArea;
    [SerializeField] private GameObject shoppingCartWindow;
    [SerializeField] private GameObject shoppingCartSpillArea;
    [SerializeField] private Text shoppingCartTotalValue;
    [SerializeField] private Text shopNameText;
    [SerializeField] private GameObject BuyButton;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip cashing;
    [SerializeField] private AudioClip error;
    
    // Variables
    [HideInInspector] public bool canBuy;

    // Shopping Cart
    public InventoryType cartItems = new ShoppingCartInventory();
    public float discountMultiplier = 1f;
    public float totalValue;

    // Shop
    private InventoryType _currentShop = null;
    private string _shopName;

    private void Awake()
    {
        UI = this;

        shoppingWindow.SetActive(false);
        shoppingCartWindow.SetActive(false);
        shoppingCartButton.SetActive(false);

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (shoppingCartWindow.activeSelf)
        {
            if (canBuy == !BuyButton.activeSelf)
            {
                BuyButton.SetActive(canBuy);
            }
        }
    }

    /// <summary>
    /// Cleans and re instantiates all items on the shopping cart
    /// </summary>
    private void UpdateCart()
    {
        if (shoppingCartWindow.activeSelf)
        {
            cartItems.UpdateItems(shoppingCartSpillArea);

            totalValue = cartItems.GetTotalValue(discountMultiplier);
            shoppingCartTotalValue.text = totalValue.ToString("n2");
        }
    }

    #region Show

    private void ShowShoppingCartWindow(bool show)
    {
        shoppingCartWindow.SetActive(show);

        if (show)
        {
            UpdateCart();
        }
        else
        {
            cartItems.DestroyAllObjects();
        }
    }

    public void ToggleShopping(bool show)
    {
        shoppingCartButton.SetActive(show);
        if (show) return;

        shoppingWindow.SetActive(false);
        shoppingCartWindow.SetActive(false);
    }
    
    private void ShowShoppingAreaWindow(bool show, InventoryType inv)
    {
        shoppingWindow.SetActive(show);
        if (show) ShowShoppingCartWindow(true);

        if (show)
        {
            _currentShop = inv;
            _currentShop.UpdateItems(shoppingWindowSpillArea);
        }
        else
        {
            _currentShop?.DestroyAllObjects();
            _currentShop = null;
        }
    }

    /// <summary>
    /// Steal all items from the cart
    /// </summary>
    public void StealItems()
    {
        foreach (var item in ShoppingUI.UI.cartItems._items) Player.P.inventory.TryAdd(item);
        cartItems._items.Clear();
    }
    
    /// <summary>
    /// Tries to buy the entire cart
    /// </summary>
    private void TryBuy()
    {
        if (SufficientFunds() && SufficientInventorySpace())
        {
            CashUI.Cash.CashFlow(-totalValue);

            foreach (var item in cartItems._items)
            {
                Player.P.inventory.TryAdd(item);
            }

            _audioSource.clip = cashing;
            _audioSource.Play();

            cartItems._items.Clear();
            UpdateCart();
        }
        else
        {
            _audioSource.clip = error;
            _audioSource.Play();
        }
    }

    /// <summary>
    ///  Checks if the player have sufficient funds
    /// </summary>
    /// <returns></returns>
    private bool SufficientFunds()
    {
        if (Player.P.cash >= totalValue) return true;

        // TODO: Blink total price
        return false;
    }

    /// <summary>
    /// Checks if the player have enough space in your inventory
    /// </summary>
    /// <returns></returns>
    private bool SufficientInventorySpace()
    {
        if (Player.P.inventory._bagSize-Player.P.inventory._items.Count >= cartItems._items.Count) return true;

        // TODO: Blink something to indicate the full bag
        return false;
    }

    #endregion
    #region Outside calling methods

    /// <summary>
    /// What happens when clicked the shopping cart button
    /// </summary>
    public void OnShoppingCartOpenButtonClick()
    {
        if (shoppingCartWindow.activeSelf)
        {
            ShowShoppingCartWindow(false);
            ShowShoppingAreaWindow(false, null);
        }
        else
        {
            ShowShoppingCartWindow(true);
        }
    }
    
    /// <summary>
    /// Opens a shop
    /// </summary>
    /// <param name="inv">Shop inventory</param>
    /// <param name="shopName">The name displayed at the top of the shop</param>
    public void OpenShoppingArea(InventoryType inv, string shopName)
    {
        _currentShop = inv;
        shopNameText.text = shopName;
        
        ShowShoppingAreaWindow(true, inv);
    }

    /// <summary>
    /// What happens when clicked the close shopping cart button
    /// </summary>
    public void OnShoppingCartCloseButtonClick()
    {
        ShowShoppingCartWindow(false);
        ShowShoppingAreaWindow(false, null);
    }
    
    /// <summary>
    /// What happens when clicking the close button from the shopping window
    /// </summary>
    public void OnShoppingAreaCloseButtonClick()
    {
        ShowShoppingAreaWindow(false, null);
    }

    /// <summary>
    /// Tries to buy all the cart
    /// </summary>
    public void OnBuyButtonClicked()
    {
        TryBuy();
    }

    /// <summary>
    /// Adds an item to the shopping cart
    /// </summary>
    /// <param name="item">The item to be added</param>
    public void AddToCart(InventoryItem item)
    {
        cartItems._items.Add(item);
        UpdateCart();
    }

    /// <summary>
    /// Removes an item to the shopping cart
    /// </summary>
    /// <param name="item">The item to be removed</param>
    public void RemoveFromCart(InventoryItem item)
    {
        cartItems._items.Remove(item);
        UpdateCart();
    }
    
    #endregion
}