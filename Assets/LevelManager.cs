/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    public Transform Level1Sp1, Level1Sp2, Level1Ap1, Level1Sp3;
    public List<GameObject> alienSpawnPoints, scorpianSpawnPoints, DroneSpawnPoints;
    int levelNo;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            levelNo = 1;
            Debug.Log("Level1");



            scorpianSpawnPoints = new List<GameObject>();


            scorpianSpawnPoints.Add(Level1Sp1.gameObject);
            scorpianSpawnPoints.Add(Level1Sp2.gameObject);
            scorpianSpawnPoints.Add(Level1Sp3.gameObject);


            Level1Fun();



        }

    }
    private void Level1Fun()
    {

        Level1Map.gameObject.SetActive(true);

        StartCoroutine(Level1TimeDelayBetweenS_Swamps());

        // We need 10 to 15 scorpians 

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
*/