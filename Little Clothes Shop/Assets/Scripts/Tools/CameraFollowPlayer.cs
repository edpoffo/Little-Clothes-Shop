using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollowPlayer : MonoBehaviour
{
    public static CameraFollowPlayer Cfp;
    
    [SerializeField] private float speedMove = 0.2f;        // The translation movement rate
    [SerializeField] private float speedZoom = 0.1f;        // The zoom movement rate
    [SerializeField] private float targetCameraSize;        // The desired final zoom
    [HideInInspector] public float defaultCameraSize;       // The camera size (zoom) the camera started with
    [SerializeField] private Vector3 offset = new Vector3(0,.75f,-15); 
    
    private void Start()
    {
        defaultCameraSize = Camera.main.orthographicSize;
        targetCameraSize = defaultCameraSize;
        Cfp = this;

        transform.position = Player.P.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
        Zoom();
    }

    /// <summary>
    /// Gradually change camera position to create a smooth movement
    /// </summary>
    private void Move()
    {
        Vector3 targetCameraPosition = Vector2.Lerp(transform.position, Player.P.transform.position + offset, speedMove);
        targetCameraPosition.z = offset.z;

        transform.position = targetCameraPosition;
    }
    
    /// <summary>
    /// Gradually change camera size to apply a zoom effect
    /// </summary>
    private void Zoom()
    {
        if (!(Camera.main == null))
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetCameraSize, speedZoom);
    }
    
    /// <summary>
    /// This method changes the camera target zoom
    /// </summary>
    /// <param name="target">The zoom amount (0 or ignored, reset camera zoom to default)</param>
    public static void ApplyZoom(float target = 0f)
    {
        Cfp.targetCameraSize = target <= 0 ? Cfp.defaultCameraSize : target;
    }
}
