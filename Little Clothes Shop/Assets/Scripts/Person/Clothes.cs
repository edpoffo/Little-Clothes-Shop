using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Serialization;

public class Clothes : MonoBehaviour
{
    public ClothingItem _clothingItem;
    
    public string spriteSheetName;
    public Color c;
    private Person _rootPerson;
    private Animator _animator;

    private SpriteRenderer _sr;
    
    Sprite[] spriteSheet;
    
    public int layer;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rootPerson = transform.parent.parent.GetComponent<Person>();
        spriteSheet = Resources.LoadAll<Sprite>("Character/Clothes/" + spriteSheetName);

        if (spriteSheet == null)
        {
            Debug.Log("Spritesheet " + spriteSheetName + " not found");
            Destroy(gameObject);
            return;
        }
        
        _rootPerson.ChangedDirection += ChangeDirection;
        _rootPerson.ForceChangeDirection();
        
        _sr.color = c;
    }

    private void OnDestroy()
    {
        _rootPerson.ChangedDirection -= ChangeDirection;
    }

    private void LateUpdate()
    {
        ChangeSprite();
    }

    /// <summary>
    /// Follow Change Direction from body animator
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    private void ChangeDirection(object source, EventArgs args)
    {
        _animator.SetFloat(_rootPerson.AnimSpeed,
            _rootPerson.moving
                ? _rootPerson.Speed
                : 0); // Currently, the Speed only affects the state, not the animation speed TODO: Animation speed with movement speed
        _animator.SetInteger(_rootPerson.AnimDirection, _rootPerson.lastDirection);
        _animator.SetTrigger(_rootPerson.AnimChangedDirection);
    }
    
    /// <summary>
    /// This method is called to remove this clothing
    /// </summary>
    /// <returns>The clothingItem containing this Clothes</returns>
    public InventoryItem RemoveClothes()
    {
        Destroy(gameObject);
        
        return _clothingItem;
    }

    /// <summary>
    /// This will change the current sprite loaded by the Animator to a similar one at another Spritesheet
    /// </summary>
    private void ChangeSprite()
    {
        if (spriteSheet.Length == 0)
        {
            Debug.LogError("Sprite Sheet Name not found at Resources/Character/Clothes/");
            return;
        }

        var newSprite = Array.Find(spriteSheet, item => item.name == _sr.sprite.name);
        if (newSprite) _sr.sprite = newSprite;
    }

    #region Static methods

    private static GameObject _clothingPrefab;

    /// <summary>
    /// Instantiates a clothing accordingly with the clothingItem
    /// </summary>
    /// <param name="clothingItem">The item containing the clothing</param>
    /// <param name="person">The person to instantiate the clothing in</param>
    /// <param name="layer">The Z layer</param>
    /// <returns>The generated Clothing</returns>
    public static Clothes InstantiateClothingObject(ClothingItem clothingItem, Person person, int layer)
    {
        const string path = "Prefabs/Clothing/ClothingPrefab";
        
        if (!_clothingPrefab) _clothingPrefab = Resources.Load<GameObject>(path);

        if (!_clothingPrefab)
        {
            Debug.Log("Prefab not found at " + path);
        }
        
        var c =  Instantiate(_clothingPrefab,person.transform.Find("Body")).GetComponent<Clothes>();
        
        c.name = clothingItem.ItemName;
        c._clothingItem = clothingItem;
        c.spriteSheetName = clothingItem.path;
        c.c = clothingItem.ItemColor;
        c.layer = layer;

        c.transform.localPosition = new Vector3(0, 0, (float)layer * -0.01f);

        return c;
    }

    /// <summary>
    /// Calls for the clothing to change layer, basically, it slightly changes de Z local position (0.01 per layer)
    /// </summary>
    /// <param name="l">New Z layer number</param>
    public void ChangeLayer(int l)
    {
        layer = l;

        var p = transform.localPosition;
        p.z = -l * 0.01f;

        transform.localPosition = p;
    }

    #endregion
}