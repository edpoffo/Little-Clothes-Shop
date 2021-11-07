using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class InventoryItem
{
    public Sprite Icon => CachedIcon;
    protected Sprite CachedIcon;
    public virtual string ItemName => "OverrideName";
    public virtual float Price => 0f;
    public abstract string ItemType { get; }
    [SerializeField] public string path;
    public virtual string Limitation
    {
        get => CachedLimitation;
        set => CachedLimitation = value;
    }

    protected string CachedLimitation = "";

    public virtual Color ItemColor
    {
        get => CachedColor;
        set => CachedColor = value;
    }
    
    public virtual void ExtraUse(){}

    protected Color CachedColor = Color.white;
    public string iconPath;
    public int iconIndex = -1; // -1 means only one sprite at path
}