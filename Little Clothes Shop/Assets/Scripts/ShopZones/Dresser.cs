using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dresser : MonoBehaviour
{
    private const float ZoomedInCameraSize = 5f;
    
    private void EnteredDresser()
    {
        CameraFollowPlayer.ApplyZoom(ZoomedInCameraSize);
        UIHandler.ShowChangeClothesWindow(true);
    }

    private void ExitedDresser()
    {
        CameraFollowPlayer.ApplyZoom();
        UIHandler.ShowChangeClothesWindow(false);
    }
    
    
    #region Triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnteredDresser();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ExitedDresser();
    }

    #endregion
    
}
