using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform smallGreenScorpians;
    [SerializeField] Transform floor;
    [SerializeField]Transform alien;
    [SerializeField] Transform drone;
    public Transform Level1Sp1, Level1Sp2, Level1Ap1;
    public List<GameObject> alienSpawnPoints, scorpianSpawnPoints, DroneSpawnPoints;
    // Start is called before the first frame update
    void Start()
    {


        if (SceneManager.GetActiveScene().name == "Level1")
        {
            alienSpawnPoints = new List<GameObject>();
            scorpianSpawnPoints = new List<GameObject>();

            alienSpawnPoints.Add( Level1Ap1.gameObject);
            scorpianSpawnPoints.Add(Level1Sp1.gameObject);
            scorpianSpawnPoints.Add(Level1Sp2.gameObject);


            Debug.Log("Level1");


            LoopTheScorpianSpawn();
            LoopTheAlienGroup();
            LoopTheDroneSpawn();
        }
        /*     LoopTheScorpianSpawn();
            // LoopTheDroneSpawn();
             LoopTheAlienGroup();*/
    }

    private void LoopTheAlienGroup()
    {
        StartCoroutine(RandomAlienGroupSpawnTimer());
    }

    private IEnumerator RandomAlienGroupSpawnTimer()
    {

        int randomAlienGroupSpwanTime = UnityEngine.Random.Range(5, 15);
        yield return new WaitForSeconds(randomAlienGroupSpwanTime);
      
        SpawnAlienGroup();
  
      //  LoopTheAlienGroup();

    }

    private void SpawnAlienGroup() {
        Vector3 SpawnAreaVector = Vector3.zero;
        if (alienSpawnPoints.Count == 1)
        {
             SpawnAreaVector = alienSpawnPoints[0].transform.position;
            scorpianSpawnPoints.Remove(alienSpawnPoints[0]);
        }
        if(alienSpawnPoints.Count>1)
        {
            foreach(GameObject a in alienSpawnPoints)
            {
                SpawnAreaVector = a.transform.position;
                alienSpawnPoints.Remove(a);
                break;
            }
        }
     
        int HowManyAliens = UnityEngine.Random.Range(4, 9);
      
        Vector3[] SpawnVectorsByIndex;
        SpawnVectorsByIndex = new Vector3[HowManyAliens];
        Vector3 singleDronePosition;

        for (int i = 0; i < HowManyAliens; i++)
        {
            singleDronePosition = new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition());
            Instantiate(alien, singleDronePosition, Quaternion.identity);
        }

    }

    private void LoopTheScorpianSpawn()
    {
        StartCoroutine(RandomScorpianSpawnTimer());
   



    }

    private void LoopTheDroneSpawn()
    {
        StartCoroutine(RandomDroneGroupSpawnTimer());



    }

    IEnumerator RandomDroneGroupSpawnTimer()
    {
        int randomDroneGroupSpwanTime = UnityEngine.Random.Range(20, 30);
        yield return new WaitForSeconds(randomDroneGroupSpwanTime);
        Debug.Log("Hi dron coming..");
        SpwanDroneGroup();
        LoopTheDroneSpawn();


    }

    private void SpwanDroneGroup()
    {

        //float[] values;
        int HowManyDrones = UnityEngine.Random.Range(2, 3);
        Vector3 SpawnAreaVector = GetRandomSpawnVector();
        Vector3[] SpawnVectorsByIndex;
        SpawnVectorsByIndex = new Vector3[HowManyDrones];
        Vector3 singleDronePosition;

        for(int i = 0; i < HowManyDrones; i++)
        {
            singleDronePosition = new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition()) ;
            Instantiate(drone, singleDronePosition, Quaternion.identity);
        }

   

        /*
                for (int i = 0; i <= HowManyDrones; i++)
                {
                    Instantiate(drone, , Quaternion.identity);
                }*/
    }




/*
    private void SpwanDroneGroup()
    {

        //float[] values;
        int HowManyDrones = UnityEngine.Random.Range(2, 3);
        Vector3 SpawnAreaVector = GetRandomSpawnVector();
        Vector3[] SpawnVectorsByIndex;
        SpawnVectorsByIndex = new Vector3[HowManyDrones];
        Vector3 singleDronePosition;

        for (int i = 0; i < HowManyDrones; i++)
        {
            singleDronePosition = new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition());
            Instantiate(alien, singleDronePosition, Quaternion.identity);
        }



        *//*
                for (int i = 0; i <= HowManyDrones; i++)
                {
                    Instantiate(drone, , Quaternion.identity);
                }*//*
    }*/

    private Vector3 GetRandomSpawnVector()
    {
        int xRandom = UnityEngine.Random.Range(-Convert.ToInt32(floor.localScale.x), Convert.ToInt32(floor.localScale.x));
        int zRandom = UnityEngine.Random.Range(-Convert.ToInt32(floor.localScale.x), Convert.ToInt32(floor.localScale.z));

        Vector3 randomSpawnPosition = new Vector3(xRandom, 0, zRandom);

        return randomSpawnPosition;
    }

    private float VariationInSpawnPosition()
    {
        float variationFactor = UnityEngine.Random.Range(-10, +10);
        return variationFactor;

    }

    IEnumerator RandomScorpianSpawnTimer()
    {
        int randomScorpianSpwanTime = UnityEngine.Random.Range(2, 5);
        yield return new WaitForSeconds(randomScorpianSpwanTime);

        SpwanGreenScorpian();
        LoopTheScorpianSpawn();


    }

 
    private void SpwanGreenScorpian()
    {

        int HowManyAliens = UnityEngine.Random.Range(3, 8);
        Vector3 SpawnAreaVector = Vector3.zero;
        if (scorpianSpawnPoints.Count == 1)
        {
            SpawnAreaVector = scorpianSpawnPoints[0].transform.position;
            scorpianSpawnPoints.Remove(scorpianSpawnPoints[0]);
        }
        if (scorpianSpawnPoints.Count > 1)
        {
            foreach (GameObject a in scorpianSpawnPoints)
            {
                SpawnAreaVector = a.transform.position;
                Debug.Log(a.name);
                scorpianSpawnPoints.Remove(a);
                break;
            }
        }

        Instantiate(smallGreenScorpians,SpawnAreaVector,Quaternion.identity);

    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
