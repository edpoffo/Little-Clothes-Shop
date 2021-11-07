using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StyleInventoryObject : InventoryObject, IPointerDownHandler
{
    private int _clicked = 0;
    private  float _clicktime = 0;
    private const float ClickDelay = 0.75f;
    
    [SerializeField] private Slider layerSlider;
    [SerializeField] private Text layerText;
    public Clothes clothing;

    private Image _bgImage;

    private static StyleInventoryObject _cached;
    public static StyleInventoryObject SelectedStyle
    {
        get => _cached;
        set
        {
            if (StoredClothingInventoryObject.SelectedStoredClothing)
                StoredClothingInventoryObject.SelectedStoredClothing = null;

            if (_cached) _cached._bgImage.color = UnselectedColor;
            _cached = value;
            if (value) value._bgImage.color = SelectedColor;
        }
    }

    private static Color UnselectedColor = new Color(.6f,.6f,.6f,1f);
    private static Color SelectedColor = new Color(.0f,.8f,.8f,1f);

    private void Start()
    {
        _bgImage = GetComponent<Image>();
        _bgImage.color = UnselectedColor;
        
        layerSlider.minValue = ChangeClothesUI.UI.MINLayer;
        layerSlider.maxValue = ChangeClothesUI.UI.MAXLayer;

        //_clothing = Player.P.currentClothes[0]; // TODO remove this test line
        
        layerSlider.value = clothing.layer;
    }

    public void OnLayerSliderChange()
    {
        if (!clothing) return;
        
        clothing.ChangeLayer((int)layerSlider.value);
        layerText.text = clothing.layer.ToString();
    }

    public override void OnClicked()
    {
        SelectedStyle = this;
    }

    public void RemoveClothing()
    {
        Player.P.RemoveClothes(clothing);
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
            
            ChangeClothesUI.UI.OnRemoveClick();
        }
        else if (_clicked > 2 || Time.time - _clicktime > ClickDelay) _clicked = 0;
    }
}
