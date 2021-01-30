using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    GameObject sliderMasterGo_;

    [SerializeField]
    GameObject sliderMusicGo_;
    
    [SerializeField]
    GameObject sliderSfxGo_;
    
    [SerializeField]
    GameObject graphicsSelector_;

    [SerializeField]
    Button back_;

    [SerializeField]
    GameObject optionsUi_;

    [SerializeField]
    MainMenuController mainMenuController_;

    [SerializeField]
    AudioMixer audioMixer_;

    private Slider sliderMaster_;
    private Slider sliderMusic_;
    private Slider sliderSfx_;
    private TMP_Dropdown graphicsDropdown_;

    #region Public CallBacks

    public void OnDisableOptionsUi()
    {
        PlaySoundButton(back_.GetComponent<AudioSource>());
        optionsUi_.GetComponent<Animator>().SetTrigger("CloseScreen");
        mainMenuController_.EnableMainMenuUi();
    }

    public void OnMasterVolumeChange(float sliderValue)
    {
        audioMixer_.SetFloat("MasterVolume", ConvertToLog(sliderValue));
        PlayerPrefs.SetFloat(PlayerPrefsLoader.MasterSoundSaveKey, ConvertToLog(sliderValue));
    }

    public void OnMusicVolumeChange(float sliderValue)
    {
        audioMixer_.SetFloat("MusicVolume", ConvertToLog(sliderValue));
        PlayerPrefs.SetFloat(PlayerPrefsLoader.MusicKSaveKey, ConvertToLog(sliderValue));
    }

    public void OnSfxVolumeChange(float sliderValue)
    {
        audioMixer_.SetFloat("SfxVolume", ConvertToLog(sliderValue));
        PlayerPrefs.SetFloat(PlayerPrefsLoader.SfxSoundSaveKey, ConvertToLog(sliderValue));
    }

    public void SetQuality(int qualityIndex)
    {
        // TODO: fix this bug -> audio plays when game starts 
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(PlayerPrefsLoader.GraphicsSettingsSaveKey, qualityIndex);
        //PlaySoundButton(graphicsSelector_.GetComponent<AudioSource>());
    }

    #endregion

    #region Private Methods

    private void EnableOptionsControler()
    {
        optionsUi_.GetComponent<Animator>().SetTrigger("OpenScreen");
    }

    private void LoadAndSetVolume()
    {
        float currentValue;
        
        currentValue = PlayerPrefs.GetFloat(PlayerPrefsLoader.MasterSoundSaveKey, 0);
        sliderMaster_.value = ConvertFromLog(currentValue);
        audioMixer_.SetFloat("MasterVolume", currentValue);

        currentValue = PlayerPrefs.GetFloat(PlayerPrefsLoader.MusicKSaveKey, 0);
        sliderMusic_.value = ConvertFromLog(currentValue);
        audioMixer_.SetFloat("MusicVolume", currentValue);

        currentValue = PlayerPrefs.GetFloat(PlayerPrefsLoader.SfxSoundSaveKey, 0);
        sliderSfx_.value = ConvertFromLog(currentValue);
        audioMixer_.SetFloat("SfxVolume", currentValue);
    }

    private void LoadAndSetGraphics()
    {
        int currentValue = PlayerPrefs.GetInt(PlayerPrefsLoader.GraphicsSettingsSaveKey, 0);
        graphicsDropdown_.value = currentValue;
        QualitySettings.SetQualityLevel(currentValue);
    }

    private float ConvertToLog(float value)
    {
        return 20.0f * Mathf.Log10(value);
    }

    private float ConvertFromLog(float value)
    {
        return Mathf.Pow(10.0f, value/20.0f);
    }

    private void PlaySoundButton(AudioSource buttonAudioSource)
    {
        buttonAudioSource.Play();
        //AudioSource.PlayClipAtPoint(buttonAudioSource.clip, Camera.main.transform.position);
    }

    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Awake()
    {
        sliderMaster_ = sliderMasterGo_.transform.Find("Slider").GetComponent<Slider>();
        sliderMusic_ = sliderMusicGo_.transform.Find("Slider").GetComponent<Slider>();
        sliderSfx_ = sliderSfxGo_.transform.Find("Slider").GetComponent<Slider>();
        graphicsDropdown_ = graphicsSelector_.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();

        if (sliderMusic_ == null || sliderSfx_ == null || graphicsSelector_ == null || sliderMaster_ == null ||
            mainMenuController_ == null || back_ == null || optionsUi_ == null || audioMixer_ == null)
        {
            Debug.LogError("Some components are null on OptionsController!");
        }
    }

    private void Start()
    {
        LoadAndSetVolume();
        LoadAndSetGraphics();
    }
    private void OnEnable()
    {
        MainMenuController.OnOptionsPress += EnableOptionsControler;
    }

    private void OnDisable()
    {
        MainMenuController.OnOptionsPress -= EnableOptionsControler;
    }

    #endregion
}
