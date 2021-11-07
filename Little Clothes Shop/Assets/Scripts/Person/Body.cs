using System;
using System.Runtime.Serialization;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Player.BodyType BodyType;

    private static Sprite[] MaleBodySpriteSheet => GetBodySpriteSheet(cachedMaleBodySpriteSheet, "MaleBody");
    private static Sprite[] cachedMaleBodySpriteSheet;
    
    private static Sprite[] FemaleBodySpriteSheet => GetBodySpriteSheet(cachedFemaleBodySpriteSheet, "FemaleBody");
    private static Sprite[] cachedFemaleBodySpriteSheet;
    
    private static Sprite[] GetBodySpriteSheet(Sprite[] cached, string spritesheetName)
    {
        if (cached != null) return cached;

        var sSheet = Resources.LoadAll<Sprite>("Character/Bodies/" + spritesheetName);

        if (sSheet == null)
        {
            Debug.Log("Spritesheet not found");
        }

        cached = sSheet;
        
        return sSheet;
    }
    
    private SpriteRenderer _sr;
    Sprite[] spriteSheet;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        ChangeSpriteSheet();
        ChangeSprite();
    }

    /// <summary>
    /// As there are multiple types of bodies, this one makes sure the animation is kept the same
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">The body type is not registered at ChangeSpriteSheet</exception>
    private void ChangeSpriteSheet()
    {
        switch (BodyType)
        {
            case Person.BodyType.Male:
                spriteSheet = MaleBodySpriteSheet;
                break;
            case Person.BodyType.Female:
                spriteSheet = FemaleBodySpriteSheet;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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

        Sprite newSprite;
        try
        {
            newSprite = Array.Find(spriteSheet, item => item.name == _sr.sprite.name);
            if (newSprite) _sr.sprite = newSprite;
        }
        catch
        {
            // ignored
        }
    }
}
