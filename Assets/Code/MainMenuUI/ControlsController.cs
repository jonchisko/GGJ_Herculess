using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsController : MonoBehaviour
{
    [SerializeField]
    Button back_;

    [SerializeField]
    GameObject controlsUi_;

    [SerializeField]
    MainMenuController mainMenuController_;

    #region Public Callbacks

    public void OnDisableControlsUi()
    {
        PlaySoundButton(back_.GetComponent<AudioSource>());
        controlsUi_.GetComponent<Animator>().SetTrigger("CloseScreen");
        mainMenuController_.EnableMainMenuUi();
    }

    #endregion

    #region Private Methods

    private void EnableControlsControler()
    {
        controlsUi_.GetComponent<Animator>().SetTrigger("OpenScreen");
    }

    private void PlaySoundButton(AudioSource buttonAudioSource)
    {
        AudioSource.PlayClipAtPoint(buttonAudioSource.clip, Camera.main.transform.position);
    }

    #endregion

    #region Unity Callbacks

    public void OnEnable()
    {
        MainMenuController.OnControlsPress += EnableControlsControler;
    }

    public void OnDisable()
    {
        MainMenuController.OnControlsPress -= EnableControlsControler;
    }

    #endregion

    #region Unity Callbacks 

    void Awake()
    {
        if (mainMenuController_ == null || controlsUi_ == null || back_ == null)
        {
            Debug.LogError("Some components are null on OptionsController!");
        }
    }

    #endregion
}
