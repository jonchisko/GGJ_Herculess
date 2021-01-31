using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuitLevel : MonoBehaviour
{

    public GameObject popupMenu;

    private void OnEnable()
    {
        ObstacleEffectsSo.OnHerculessDeath += OnHerculessDeath;
        ObstacleEffectsSo.OnHerculessWin += OnHerculessWin;
    }

    private void OnDisable()
    {
        ObstacleEffectsSo.OnHerculessDeath -= OnHerculessDeath;
        ObstacleEffectsSo.OnHerculessWin -= OnHerculessWin;
    }

    private void OnHerculessDeath()
    {
        popupMenu.SetActive(true);
        // pause game
        PausePopUp();
        // open "popup"
        OpenLossPopup();
    }

    private void OnHerculessWin()
    {
        popupMenu.SetActive(true);
        // pause game
        PausePopUp();
        // open "popup"
        OpenWinPopup();
    }

    private void PausePopUp()
    {
        Time.timeScale = 0f;
    }

    private void OpenWinPopup()
    {
        popupMenu.GetComponentInChildren<TextMeshProUGUI>().text = "YOU WON! CONGRATS!";
    }

    private void OpenLossPopup()
    {
        popupMenu.GetComponentInChildren<TextMeshProUGUI>().text = "BETTER LUCK NEXT TIME!";
    }

    public void ToMainMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

}
