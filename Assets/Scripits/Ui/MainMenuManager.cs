using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MainMenuManager : MonoBehaviour
{
     [Header("Panels")]
     public GameObject MainMenuPanel;
     public GameObject SharePanel;
     public GameObject SettingsPanel;
     public GameObject SoundOnButton;
     public GameObject SoundOffButton;
     public GameObject MusicOnButton;
     public GameObject MusicOffButton;
     public GameObject ShopPanel;
     public GameObject LevelPanel;
     public GameObject instructionPanel;
     public GameObject LevelPanelPart2;



     private CanvasGroup MainMenuPanelCanvasGroup;


     public void Start()
     {
          MainMenuPanelCanvasGroup = MainMenuPanel.GetComponent<CanvasGroup>();
     }

     public void MainMenuToSharePanel()
     {
          SharePanel.SetActive(true);
          HideBackPanel();
     }

     public void SharePanelToMainMenuPanel()
     {
          SharePanel.SetActive(false);
          ShowBackPanel();
     }

     public void MainMenuToSettingsPanel()
     {
          SettingsPanel.SetActive(true);
          HideBackPanel();
     }

     public void SettingsToMainMenuPanel()
     {
          SettingsPanel.SetActive(false);
          ShowBackPanel();
     }

     public void SoundOn()
     {
          SoundOffButton.SetActive(false);
          SoundOnButton.SetActive(true);
     }

     public void SoundOff()
     {
          SoundOnButton.SetActive(false);
          SoundOffButton.SetActive(true);
     }

     public void MusicOn()
     {
          MusicOffButton.SetActive(false);
          MusicOnButton.SetActive(true);
     }

     public void MusicOff()
     {
          MusicOnButton.SetActive(false);
          MusicOffButton.SetActive(true);
     }


     public void MainMenuToShopPanel()
     {
          ShopPanel.SetActive(true);
          HideBackPanel();
     }

     public void ShopToMainMenuPanel()
     {
          ShopPanel.SetActive(false);
          ShowBackPanel();
     }

     public void MainMenuToLevelPanel()
     {
          MainMenuPanel.SetActive(false);
          LevelPanel.SetActive(true);
     }

     public void LevelToMainMenuPanel()
     {
          LevelPanel.SetActive(false);
          MainMenuPanel.SetActive(true);
     }

     public void LevelPanel1ToLevelPanel2()
     {
          instructionPanel.SetActive(false);
          LevelPanelPart2.SetActive(true);
     }

     public void LevelPanel2ToLevelPanel1()
     {
          instructionPanel.SetActive(true);
          LevelPanelPart2.SetActive(false);
     }

    public void InstructionPanel()
    {
        instructionPanel.SetActive(true);
    }



    public void LevelToLoadingScene()
     {
       /*   GameObject currentLevelBtn = EventSystem.current.currentSelectedGameObject.gameObject;
          string currentLevelText = currentLevelBtn.name;
          int levelValue = Int32.Parse(currentLevelText);
          PlayerPrefs.SetInt("LevelValue", levelValue);
          Debug.Log("PlayerPrefLevelValue: " + PlayerPrefs.GetInt("LevelValue"));*/
          SceneManager.LoadScene(1);
     }



     void HideBackPanel()
     {
          MainMenuPanelCanvasGroup.alpha = 0.75f;
          MainMenuPanelCanvasGroup.interactable = false;
          MainMenuPanelCanvasGroup.blocksRaycasts = false;
     }

     void ShowBackPanel()
     {
          MainMenuPanelCanvasGroup.alpha = 1;
          MainMenuPanelCanvasGroup.interactable = true;
          MainMenuPanelCanvasGroup.blocksRaycasts = true;
     }

     public void QuitGame()
     {
          Application.Quit();
     }

}
