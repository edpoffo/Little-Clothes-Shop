using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheftBlocker : MonoBehaviour
{
    private AudioSource _alarmAudioSource;
    [SerializeField] private ShopInside shop; 

    private void Awake()
    {
        _alarmAudioSource = GetComponent<AudioSource>();
    }

    #region Triggers

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ShoppingUI.UI.cartItems._items.Count > 0)
            {
                if(shop.playerInside) _alarmAudioSource.Stop();
                else
                {
                    ShoppingUI.UI.StealItems();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ShoppingUI.UI.cartItems._items.Count > 0)
            {
                _alarmAudioSource.Play();
                _alarmAudioSource.time = 3f;
            }
        }
    }

    #endregion
    
}
