using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameplayUIManager : MonoBehaviour
{
     [Header("Panels")]
     public GameObject levelCompletePanel;
     public GameObject levelFailedPanel;
     public GameObject LevelPausePanel;
     public GameObject CongratulationsPanel;
    public TextMeshProUGUI shotGunUnlock,laserGunUnlock;
    public static GameplayUIManager Instance;
    public GameObject joystickCanvaus;
    public GameObject gunPanel;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
         joystickCanvaus.SetActive(false);
           gunPanel.gameObject.SetActive(false);
        }
        Instance = this;
  //      ShiedDestructionEvent.Instance.OnDestructionOfLastPart += LevelFailedPanel;
       // Enemy.Instance.OnGameOver += LevelFailPanelDueToPlayerDeath;
    }

    public void LevelFailedPanel()
    {
        joystickFum.Instance.gameObject.SetActive(false);
        levelFailedPanel.SetActive(true);
    }
    public void GameplayToPausePanel()
     {
        joystickFum.Instance.gameObject.SetActive(false);
        Time.timeScale = 0f;
          LevelPausePanel.SetActive(true);
     }

     public void PauseToGameplayPanel()
     {
        joystickFum.Instance.gameObject.SetActive(true);
        Time.timeScale = 1f;
        LevelPausePanel.SetActive(false);
     }


    public void GameToHome()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene i = SceneManager.GetActiveScene();
        int sceneNo = i.buildIndex;
        SceneManager.LoadScene(sceneNo);
    }
    public void LevelCompletePanel()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            laserGunUnlock.gameObject.SetActive(true);
        }
        if (SceneManager.GetActiveScene().name == "Level5")
        {
            shotGunUnlock.gameObject.SetActive(true);
        }
       
        Time.timeScale = 0;
        levelCompletePanel.SetActive(true);

    }
    public void NextLevel()
    {

        if (SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level5")
        {
            laserGunUnlock.gameObject.SetActive(false);
            shotGunUnlock.gameObject.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "Level6")
        {
            SceneManager.LoadScene("Level1");
        }
        Time.timeScale = 1f;
        Scene i = SceneManager.GetActiveScene();
        int sceneNo = i.buildIndex;

        SceneManager.LoadScene(sceneNo + 1);
    }

}
