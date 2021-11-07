using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UiWindow
{
    public static InventoryUI UI;
    protected override bool CloseOnCancel => true;
    
    [SerializeField] private GameObject spillArea;
    [SerializeField] private Text filText;
    
    private void Start()
    {
        UI = this;
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        Player.P.inventory.UpdateItems(spillArea);

        filText.text = Player.P.inventory._items.Count.ToString("d2") + "/" + Player.P.inventory._bagSize.ToString("d2");
    }

    #region Enable/Disable

    protected override void OnEnable()
    {
        UpdateInventory();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        Player.P.inventory.DestroyAllObjects();
        base.OnDisable();
    }

    #endregion
    
}
