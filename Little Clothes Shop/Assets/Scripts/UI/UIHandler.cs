using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class UIHandler : MonoBehaviour
{
    public static UIHandler UI;
    
    [SerializeField] private GameObject inventoryWindow;
    [SerializeField] private GameObject changeClothesUI;
    [SerializeField] private GameObject pauseUI;

    private void Awake()
    {
        UI = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Player.Inventory)) ToggleInventoryWindow(); // Triggers all use actions
        if (Input.GetKeyDown(Player.Cancel))
        {
            var closingWindows = UiWindow.OpenedUI.ToList();
            if (closingWindows.Count > 0) foreach (var window in closingWindows) window.CancelAction();
            else pauseUI.SetActive(true);
        }
    }

    private void ToggleInventoryWindow()
    {
        inventoryWindow.SetActive(!inventoryWindow.activeSelf);
    }

    public static void ShowChangeClothesWindow(bool show)
    {
        if(UI.changeClothesUI.activeSelf!=show) UI.changeClothesUI.SetActive(show);
    }

}
