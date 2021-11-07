using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioHandler : MonoBehaviour
{
    public static ScenarioHandler Instance;
    
    private AudioSource scenarioBGM;
    
    private float _volume = 0f;
    public const float MAXVolume = .5f;
    private float targetVolume = .5f;
    private const float TransitionTime = 1f;

    private void Awake()
    {
        scenarioBGM = GetComponent<AudioSource>();
        Instance = this;
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
            case true when scenarioBGM.isPlaying:
                scenarioBGM.Pause();
                break;
            case false when !scenarioBGM.isPlaying:
                scenarioBGM.UnPause();
                break;
        }
    }

    /// <summary>
    /// Gradually fades the song
    /// </summary>
    private void FadeShop()
    {
        if (_volume == targetVolume) return;

        if (Mathf.Abs(targetVolume - _volume) < Time.fixedDeltaTime / TransitionTime) _volume = targetVolume;

        var step = Time.fixedDeltaTime / TransitionTime;
        var sign = Mathf.Sign(targetVolume - _volume);
        _volume += sign * step;

        scenarioBGM.volume = _volume;
    }

    /// <summary>
    /// Changes the BGM target volume
    /// </summary>
    /// <param name="volume"></param>
    public static void SetTargetBGMVolume(float volume = MAXVolume)
    {
        Instance.targetVolume = volume;
    }
}
