using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class StoredClothingInventoryObject : InventoryObject, IPointerDownHandler
{
    private int _clicked = 0;
    private  float _clicktime = 0;
    private const float ClickDelay = 0.75f;

    private Image _bgImage;

    private static StoredClothingInventoryObject _cached;
    public static StoredClothingInventoryObject SelectedStoredClothing
    {
        get => _cached;
        set
        {
            if(StyleInventoryObject.SelectedStyle) StyleInventoryObject.SelectedStyle = null;
            
            if(_cached) _cached._bgImage.color = UnselectedColor;
            _cached = value;
            if(value) value._bgImage.color = SelectedColor;
        }
    }

    private static Color UnselectedColor = new Color(.6f,.6f,.6f,1f);
    private static Color SelectedColor = new Color(.0f,.8f,.8f,1f);

    private void Awake()
    {
        _bgImage = GetComponent<Image>();
        _bgImage.color = UnselectedColor;
    }

    public override void OnClicked()
    {
        SelectedStoredClothing = this;
    }

    public void Wear()
    {
        Item.ExtraUse();
        ChangeClothesUI.UI.UpdateInventories();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _clicked++;

        if (_clicked == 1)
        {
            OnClicked();
            _clicktime = Time.time;
        }
 
        if (_clicked > 1 && Time.time - _clicktime < ClickDelay)
        {
            _clicked = 0;
            _clicktime = 0;
            
            ChangeClothesUI.UI.OnWearClick();
        }
        else if (_clicked > 2 || Time.time - _clicktime > ClickDelay) _clicked = 0;
    }
}
