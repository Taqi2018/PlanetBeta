using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
     [Header("Panels")]
     public GameObject LevelCompletePanel;
     public GameObject LevelFailedPanel;
     public GameObject LevelPausePanel;
     public GameObject CongratulationsPanel;


     public void GameplayToPausePanel()
     {
          LevelPausePanel.SetActive(true);
     }

     public void PauseToGameplayPanel()
     {
          LevelPausePanel.SetActive(false);
     }


     public void GameToHome()
     {
          SceneManager.LoadScene(0);
     }

}
