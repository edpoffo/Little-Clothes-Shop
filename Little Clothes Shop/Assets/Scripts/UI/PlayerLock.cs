using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : MonoBehaviour
{
    private void OnEnable()
    {
        // Debug.Log(gameObject.name + " Locked the player");
        Player.P.locked++;
    }

    private void OnDisable()
    {
        // Debug.Log(gameObject.name + " Unlocked the player");
        Player.P.locked--;
    }
}
