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
    public Transform Level1Sp1, Level1Sp2, Level1Ap1,Level1Sp3;
    public List<GameObject> alienSpawnPoints, scorpianSpawnPoints, DroneSpawnPoints;
    public ParticleSystem alienSpawnEffect;
    int levelNo;
    private int level1ScorpianSwamps;
    public Transform Level1Map;
    int noOfSwampsOfLevel1;

    // Start is called before the first frame update
    void Start()
    {



        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            levelNo = 1;
            Debug.Log("Level1");

     


        }

    }


    void Level1Loader()
    {
        noOfSwampsOfLevel1 = 3;
        scorpianSpawnPoints = new List<GameObject>();


        scorpianSpawnPoints.Add(Level1Sp1.gameObject);
        scorpianSpawnPoints.Add(Level1Sp2.gameObject);
        scorpianSpawnPoints.Add(Level1Sp3.gameObject);

        Level1Map.gameObject.SetActive(true);
        StartCoroutine(Level1TimeDelayBetweenS_Swamps());

    }


    //Rules S=1-->3  A=5->8  D=-->5->12






    IEnumerator Level1TimeDelayBetweenS_Swamps()
    {
        noOfSwampsOfLevel1--;
        int randomScorpianSpwanTime = UnityEngine.Random.Range(3, 6);
        yield return new WaitForSeconds(randomScorpianSpwanTime);
        ProduceLevel1Scorpian();
    }



    void ProduceLevel1Scorpian()
    {
        int HowManyAliens = 5;
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

                scorpianSpawnPoints.Remove(a);
                break;
            }
        }

        for (int i = 0; i < HowManyAliens; i++)
        {

            Instantiate(smallGreenScorpians, new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition()), Quaternion.identity);
        }

        if (noOfSwampsOfLevel1 > 0)
        {
            StartCoroutine(Level1TimeDelayBetweenS_Swamps());
        }


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


            int HowManyAliens = UnityEngine.Random.Range(4, 10);

            Vector3[] SpawnVectorsByIndex;
            SpawnVectorsByIndex = new Vector3[HowManyAliens];
            Vector3 singleDronePosition;

            for (int i = 0; i < HowManyAliens; i++)
            {
                singleDronePosition = new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition());

                Instantiate(alien, singleDronePosition, Quaternion.identity);

                Instantiate(alienSpawnEffect, singleDronePosition+Vector3.down*5, Quaternion.identity);
                alienSpawnEffect.Play();

            }
        }


        if(alienSpawnPoints.Count>1)
        {
            foreach(GameObject a in alienSpawnPoints)
            {
                SpawnAreaVector = a.transform.position;
                alienSpawnPoints.Remove(a);
                break;
            }


            int HowManyAliens = UnityEngine.Random.Range(3, 6);

            Vector3[] SpawnVectorsByIndex;
            SpawnVectorsByIndex = new Vector3[HowManyAliens];
            Vector3 singleDronePosition;

            for (int i = 0; i < HowManyAliens; i++)
            {
                singleDronePosition = new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition());
                Instantiate(alien, singleDronePosition, Quaternion.identity);
                Instantiate(alienSpawnEffect, singleDronePosition+Vector3.down*5, Quaternion.identity);
                alienSpawnEffect.Play();
            }
        }
   

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
        int HowManyDrones = UnityEngine.Random.Range(5, 7);
        Vector3 SpawnAreaVector = GetRandomSpawnVector();
        Vector3[] SpawnVectorsByIndex;
        SpawnVectorsByIndex = new Vector3[HowManyDrones];
        Vector3 singleDronePosition;

        for(int i = 0; i < HowManyDrones; i++)
        {
            singleDronePosition = new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition()) ;
            Instantiate(drone, singleDronePosition, Quaternion.identity);
        }

   

    }




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
        int randomScorpianSpwanTime = UnityEngine.Random.Range(3, 6);
        yield return new WaitForSeconds(randomScorpianSpwanTime);

        if (levelNo == 1)
        {
            if (level1ScorpianSwamps <= 5)
            {
                level1ScorpianSwamps++;


                if (scorpianSpawnPoints.Count > 0)
                {
                    SpwanGreenScorpian();
                    LoopTheScorpianSpawn();
                }
                Debug.Log("Swamp OF LEVEL 1");
            }
            else
            {
                levelNo = 2;
                Debug.Log("No enemy left");
            }
            
        }
        else
        {

            if (scorpianSpawnPoints.Count > 0)
            {
                SpwanGreenScorpian();
                LoopTheScorpianSpawn();
            }

        }


    }
    private void LoopTheScorpianSpawn()
    {
        StartCoroutine(RandomScorpianSpawnTimer());




    }


    private void SpwanGreenScorpian()
    {

        int HowManyAliens = UnityEngine.Random.Range(1, 1);
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
           
                scorpianSpawnPoints.Remove(a);
                break;
            }
        }

        for (int i = 0; i < HowManyAliens; i++)
        {
           
            Instantiate(smallGreenScorpians, new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition()), Quaternion.identity);
        }

        if (levelNo == 1)
        {
            LevelLoader(1);
        }
    }

    void LevelLoader(int LevelNo)
    {
        if (levelNo == 1)
        {
            RandomScorpianSpawnTimer();
        }
        if (levelNo == 2)
        {

        }
        
        if (levelNo == 3)
        {

        }
        if (levelNo == 4)
         {

        }


        if (levelNo == 3)
        {

       }
       if (levelNo == 4){

         }


        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }



}
