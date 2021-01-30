using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public delegate void BasicMenuButtonEvent();
    public static BasicMenuButtonEvent OnEventPlayPress;
    public static BasicMenuButtonEvent OnOptionsPress;
    public static BasicMenuButtonEvent OnControlsPress;


    [SerializeField]
    Button playButton_;
    
    [SerializeField]
    Button controlsButton_;

    [SerializeField]
    Button optionsButton_;
    
    [SerializeField]
    Button quitButton_;

    [SerializeField]
    GameObject mainMenuUi_;


    #region Public Methods 

    public void EnableMainMenuUi()
    {
        //AudioSource.PlayClipAtPoint(mainMenuUi_.GetComponent<AudioSource>().clip, Camera.main.transform.position);
        mainMenuUi_.GetComponent<Animator>().SetTrigger("OpenScreen");
    }

    #endregion

    #region Private Methods

    private void DisableMainMenuUi()
    {
        //AudioSource.PlayClipAtPoint(mainMenuUi_.GetComponent<AudioSource>().clip, Camera.main.transform.position);
        mainMenuUi_.GetComponent<Animator>().SetTrigger("CloseScreen");
    }

    private void PlaySoundButton(AudioSource buttonAudioSource)
    {
        AudioSource.PlayClipAtPoint(buttonAudioSource.clip, Camera.main.transform.position);
    }

    #endregion

    #region Public CallBacks

    public void OnPlayButtonPress()
    {
        // load cutscene scene
        PlaySoundButton(playButton_.GetComponent<AudioSource>());
        DisableMainMenuUi();
        OnEventPlayPress?.Invoke();
    }

    public void OnControlsButtonPress()
    {
        PlaySoundButton(controlsButton_.GetComponent<AudioSource>());
        DisableMainMenuUi();
        OnControlsPress?.Invoke();
    }

    public void OnOptionsButtonPress()
    {
        PlaySoundButton(optionsButton_.GetComponent<AudioSource>());
        DisableMainMenuUi();
        OnOptionsPress?.Invoke();
    }

    public void OnQuitButtonPress()
    {
        PlaySoundButton(quitButton_.GetComponent<AudioSource>());
        Application.Quit(0);
    }

    #endregion

    #region Unity CallBacks

    private void Awake()
    {
        if(playButton_ == null || controlsButton_ == null || optionsButton_ == null || quitButton_ == null || mainMenuUi_ == null)
        {
            Debug.LogError("Some components are null on MainMenuController!");
        }
    }

    #endregion
}
