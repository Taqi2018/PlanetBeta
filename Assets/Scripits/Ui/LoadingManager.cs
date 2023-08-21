using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
     // Start is called before the first frame update
     void Start()
     {
          StartCoroutine(GoToGameplay());
     }

     public IEnumerator GoToGameplay()
     {
        Time.timeScale = 1f;
          yield return new WaitForSeconds(1.5f);
          SceneManager.LoadScene(2);
     }

}
