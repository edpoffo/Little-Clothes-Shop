using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ShopInside : MonoBehaviour
{
    // Components
    [SerializeField] private List<Tilemap> _tileSetsToFade = new List<Tilemap>();
    private AudioSource ShopSong; 
    
    // Misc variables
    [HideInInspector] public bool playerInside;
    public string shopName;
    
    // Fade variables
    private float _alpha = 1f;
    private float targetAlpha = 1f;
    private const float TransitionTime = 1f;

    private void Awake()
    {
        ShopSong = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Everything that happens when the player enters the shop
    /// </summary>
    private void EnteredShop()
    {
        WelcomeUI.Play(shopName);                // Display the welcome UI
        ShoppingUI.UI.ToggleShopping(true); // Allow the Shopping UI to be used
        playerInside = true;                     // Triggers all effects for when the player is inside the shop
        ScenarioHandler.SetTargetBGMVolume(0f);  // Turn off the Scenario BGM
        FadeTo(0f);                         // Fade out walls and fade in shop song
    }

    /// <summary>
    /// Everything that happens when the player exits the shop
    /// </summary>
    private void ExitedShop()
    {
        ShoppingUI.UI.ToggleShopping(false);                      // Disable the shopping UI
        playerInside = false;                                          // Triggers all effects for when the player is outside the shop
        ScenarioHandler.SetTargetBGMVolume(ScenarioHandler.MAXVolume); // Turn on the Scenario BGM
        FadeTo(1f);                                               // Fade in walls and fade out shop song
        
    }

    private void FixedUpdate()
    {
        FadeShop();
    }

    private void Update()
    {
        CheckPaused();
    }

    /// <summary>
    /// If the came is paused, pause the BGM as well
    /// </summary>
    private void CheckPaused()
    {
        switch (PauseUI.paused)
        {
            case true when ShopSong.isPlaying:
                ShopSong.Pause();
                break;
            case false when !ShopSong.isPlaying:
                ShopSong.UnPause();
                break;
        }
    }

    /// <summary>
    /// Set the target alpha/volume
    /// </summary>
    private void FadeTo(float target)
    {
        targetAlpha = target;
    }

    /// <summary>
    /// Gradually fades the tilemaps so the player can see inside
    /// Also fades the song
    /// </summary>
    private void FadeShop()
    {
        if (_alpha == targetAlpha) return;

        if (Mathf.Abs(targetAlpha - _alpha) < Time.fixedDeltaTime / TransitionTime) _alpha = targetAlpha;

        var step = Time.fixedDeltaTime / TransitionTime;
        var sign = Mathf.Sign(targetAlpha - _alpha);
        _alpha += sign * step;

        foreach (var ts in _tileSetsToFade)
        {
            var color = ts.color;
            color.a = _alpha;
            ts.color = color;
        }

        ShopSong.volume = 1-_alpha;
    }

    #region Trigger Events

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) EnteredShop();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) ExitedShop();
    }

    #endregion
}