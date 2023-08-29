using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spawner: MonoBehaviour
{



    int randomItem;
    [SerializeField] private Transform ammoPrefab, medPackPrefab;
    [SerializeField] private float spawnTimer;

    List<Transform> spawnObjects;
    void Start()
    {

        spawnObjects = new List<Transform>();
        spawnObjects.Add(medPackPrefab);
        spawnObjects.Add(ammoPrefab);
   

      



        StartCoroutine(Spawn());
        
    }

    IEnumerator Spawn()
    {

        if (SceneManager.GetActiveScene().name == "Level1"  || SceneManager.GetActiveScene().name == "Level2")
        {
         
            randomItem = 0;
        }
        else if(SceneManager.GetActiveScene().name == "Level4"  )
        {
            randomItem = 1;
        }
     
        else if (SceneManager.GetActiveScene().name == "Level6")
        {
            randomItem = 1;
            yield return new WaitForSeconds(spawnTimer);
            Instantiate(spawnObjects[randomItem], transform.position + Vector3.right * 2, Quaternion.identity);
            Instantiate(spawnObjects[0], transform.position + Vector3.right * 2, Quaternion.identity);

        }
        else
        {
            randomItem = Random.Range(0, spawnObjects.Count);

        }
        yield return new WaitForSeconds(spawnTimer);
        Instantiate(spawnObjects[randomItem], transform.position, Quaternion.identity);

        if (SceneManager.GetActiveScene().name == "Level1" )
        {
            if (!Player.Instance.giveHealthNews)
            {
                Player.Instance.giveHealthNews = true;
                if (Player.Instance.giveHealthNews)
                {
                    joystickFum.Instance.gameObject.SetActive(false);
                    GameplayUIManager.Instance.gunPanel.gameObject.SetActive(false);
                    Time.timeScale = 0;
                    Player.Instance.messanger.gameObject.SetActive(true);
                    Player.Instance.healthInstruction.gameObject.SetActive(true);

                }
            }
        }


        if (SceneManager.GetActiveScene().name == "Level3" && randomItem==1)
        {
            Debug.Log("Giving ammo");
            if (!Player.Instance.giveAmmoNews)
            {
                Player.Instance.giveAmmoNews = true;
                if (Player.Instance.giveAmmoNews)
                {
                  //  joystickFum.Instance.gameObject.SetActive(false);
                    GameplayUIManager.Instance.gunPanel.gameObject.SetActive(false);
                    Time.timeScale = 0;
                    Player.Instance.messanger.gameObject.SetActive(true);
                    Player.Instance.ammoInstruction.gameObject.SetActive(true);

                }
            }
        }

    }

   
}










