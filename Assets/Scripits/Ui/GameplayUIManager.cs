using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameplayUIManager : MonoBehaviour
{
     [Header("Panels")]
     public GameObject levelCompletePanel;
     public GameObject levelFailedPanel;
     public GameObject LevelPausePanel;
     public GameObject CongratulationsPanel;
    public static GameplayUIManager Instance;
    public Canvas joystickCanvaus;

    private void Start()
    {
        Instance = this;
  //      ShiedDestructionEvent.Instance.OnDestructionOfLastPart += LevelFailedPanel;
       // Enemy.Instance.OnGameOver += LevelFailPanelDueToPlayerDeath;
    }

    public void LevelFailedPanel()
    {
        joystickCanvaus.gameObject.SetActive(false);
        levelFailedPanel.SetActive(true);
    }
    public void GameplayToPausePanel()
     {
        joystickCanvaus.transform.gameObject.SetActive(false);
        Time.timeScale = 0f;
          LevelPausePanel.SetActive(true);
     }

     public void PauseToGameplayPanel()
     {
        joystickCanvaus.transform.gameObject.SetActive(true);
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
        joystickCanvaus.gameObject.SetActive(false);
        Time.timeScale = 0;
        levelCompletePanel.gameObject.SetActive(true);
    }
    public void NextLevel()
    {
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
