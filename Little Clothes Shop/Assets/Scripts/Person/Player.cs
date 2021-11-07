using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;

public class Player : Person
{
    public static Player P;
    
    #region Setup
    
    public static BodyType PlayerBodyType = BodyType.Male;
    [HideInInspector] public Body body;
    public override float Speed => 200f;

    public PersonalInventory inventory = new PersonalInventory();
    
    // Controls
    public static KeyCode Up = KeyCode.W;
    public static KeyCode Left = KeyCode.A;
    public static KeyCode Right = KeyCode.D;
    public static KeyCode Down = KeyCode.S;
    public static KeyCode Cancel = KeyCode.Escape;
    public static KeyCode Inventory = KeyCode.Q;
    public static KeyCode Use = KeyCode.E;

    // Start is called at the first frame
    protected override void Awake()
    {
        P = this;

        base.Awake();
        body = GetComponentInChildren<Body>();

        body.BodyType = PlayerBodyType;
        
        switch (body.BodyType)
        {
            case BodyType.Male:
                Wear(new ClothingItem("Sunglasses", 150, "Character/Clothes/Sunglasses", 32, Color.white, "Sunglasses", null),3);
                Wear(new ClothingItem("Green Shirt", 150, "Character/Clothes/MaleLongSleeveShirt", 32, Color.green, "MaleLongSleeveShirt", "Male"),1);
                Wear(new ClothingItem("Red pants", 150, "Character/Clothes/Pants", 32, Color.red, "Pants", null),1);
                break;
            case BodyType.Female:
                Wear(new ClothingItem("Sunglasses", 150, "Character/Clothes/Sunglasses", 32, Color.white, "Sunglasses", null),3);
                Wear(new ClothingItem("Green female long sleeved Blouse", 150, "Character/Clothes/FemaleLongsleevedBlouse", 32, Color.green, "FemaleLongsleevedBlouse", "Female"),1);
                Wear(new ClothingItem("Red pants", 150, "Character/Clothes/Pants", 32, Color.red, "Pants", null),1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    #endregion
    #region Event Handlers

    // Delegates
    public delegate void UsedHandler(object source, EventArgs args);

    // Events
    public event UsedHandler Used;

    // Calling Event Methods
    protected virtual void OnUsed(object source)
    {
        Used?.Invoke(source, EventArgs.Empty);
    }

    #endregion
    #region Gameplay

    [HideInInspector] public float cash = 10000; // Player cash TODO: Load cash player from save file
    [HideInInspector] public int locked;  // How many things are locking the player from moving
    
    private Vector3 _cachedMovement;

    private void FixedUpdate()
    {
        Move(_cachedMovement);
    }

    // Update is called at a fixed rate
    private void Update()
    {
        if (locked==0)
        {
            _cachedMovement = MovingDirection();             // Cache the input direction
            if(Input.GetKeyDown(Use)) OnUsed(this);    // Triggers all use actions
        }
        else
        {
            _cachedMovement = Vector3.zero;
        }
    }

    #endregion
    #region Movement

    /// <summary>
    /// Get the current keys for movement and outputs the moving direction (Maybe change later)
    /// </summary>
    /// <returns>A unitary Vector3 point towards the direction </returns>
    private static Vector3 MovingDirection()
    {
        // X
        var x = (Input.GetKey(Left) ? -1 : 0) + (Input.GetKey(Right) ? +1 : 0);
        // Y
        var y = (Input.GetKey(Down) ? -1 : 0) + (Input.GetKey(Up) ? +1 : 0);

        return new Vector3(x, y, 0).normalized;
        //return new Vector3(x, y, 0);
    }

    #endregion
    #region Clothing handling

    public List<Clothes> currentClothes;

    public void Wear(ClothingItem clothingItem, int layer)
    {
        currentClothes.Add(Clothes.InstantiateClothingObject(clothingItem, this, layer));
    }

    /// <summary>
    /// Remove a worn clothing and add it to the inventory
    /// </summary>
    /// <param name="clothing">The clothing instance to be removed</param>
    public void RemoveClothes(Clothes clothing)
    {
        if (!inventory.HasFreeSlot()) return;
        inventory.TryAdd(clothing.RemoveClothes()); // the RemoveClothes() returns a item containing the clothing, and this is put into the inventory
        currentClothes.Remove(clothing);
    }

    #endregion
}