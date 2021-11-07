using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseUI : UiWindow
{
    public static bool paused;
    
    [SerializeField] private GameObject mainPage;
    [SerializeField] private GameObject instructionsPage;
    [SerializeField] private GameObject switchGenderPage;
    [SerializeField] private GameObject optionsPage;

    [SerializeField] private AudioMixer mixerSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider uISlider;

    #region Main Page

    public void OnContinueClick()
    {
        gameObject.SetActive(false);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    #region Switch gender page

    public void ChangeGender(string gender)
    {
        Person.BodyType bodyType;

        switch (gender)
        {
            case "Male":
                bodyType = Person.BodyType.Male;
                break;
            case "Female":
                bodyType = Person.BodyType.Female;
                break;
            default:
                return;
        }
        
        Player.PlayerBodyType = bodyType;
        Player.P.body.BodyType = bodyType;
    }

    #endregion

    #region Options page

    public void ChangeMaster(float value)
    {
        ChangeVolume("Master volume", value);
    }
    
    public void ChangeBGM(float value)
    {
        ChangeVolume("BGM volume", value);
    }
    
    public void ChangeEffects(float value)
    {
        ChangeVolume("Effects volume", value);
    }
    
    public void ChangeUI(float value)
    {
        ChangeVolume("UI volume", value);
    }

    private void ChangeVolume(string exposedMixerVariable, float value)
    {
        mixerSlider.SetFloat(exposedMixerVariable, Mathf.Log10(value) * 20);
    }

    #endregion

    #region Any page

    public void SwitchPage(GameObject pageObject)
    {
        CloseAllPages();
        pageObject.SetActive(true);
    }
    
    #endregion

    private void CloseAllPages()
    {
        mainPage.SetActive(false);
        instructionsPage.SetActive(false);
        switchGenderPage.SetActive(false);
        optionsPage.SetActive(false);
    }
    
    private void GetCurrentAudioVolumes()
    {
        masterSlider.value = GetVolumeFromMixer("Master volume");
        bgmSlider.value = GetVolumeFromMixer("BGM volume");
        effectsSlider.value = GetVolumeFromMixer("Effects volume");
        uISlider.value = GetVolumeFromMixer("UI volume");
    }

    private float GetVolumeFromMixer(string exposedVariable)
    {
        mixerSlider.GetFloat(exposedVariable,out var v);
        
        v = Mathf.Pow(Mathf.Pow(10, v), 1f/20f);
        return v;
    }

    #region Enabled/Disabled

    protected override void OnEnable()
    {
        paused = true;
        
        mainPage.SetActive(true);
        base.OnEnable();

        GetCurrentAudioVolumes();
        
        Time.timeScale = 0f;
    }

    protected override void OnDisable()
    {
        paused = false;
        
        CloseAllPages();
        base.OnDisable();
        
        Time.timeScale = 1f;
    }

    #endregion
}
