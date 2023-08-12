using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform smallGreenScorpians;
    [SerializeField] Transform floor;
    [SerializeField]Transform alien;
    [SerializeField] Transform drone;

    // Start is called before the first frame update
    void Start()
    {
    // LoopTheScorpianSpawn();
       // LoopTheDroneSpawn();
        LoopTheAlienGroup();
    }

    private void LoopTheAlienGroup()
    {
        StartCoroutine(RandomAlienGroupSpawnTimer());
    }

    private IEnumerator RandomAlienGroupSpawnTimer()
    {

        int randomDroneGroupSpwanTime = UnityEngine.Random.Range(10, 30);
        yield return new WaitForSeconds(randomDroneGroupSpwanTime);
        Debug.Log("Hi dron coming..");
        SpawnAlienGroup();
  
      //  LoopTheAlienGroup();

    }

    private void SpawnAlienGroup()
    {
        int HowManyDrones = UnityEngine.Random.Range(2, 10);
        Vector3 SpawnAreaVector = GetRandomSpawnVector();
        Vector3[] SpawnVectorsByIndex;
        SpawnVectorsByIndex = new Vector3[HowManyDrones];
        Vector3 singleDronePosition;

        for (int i = 0; i < HowManyDrones; i++)
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
        float variationFactor = UnityEngine.Random.Range(-2, +2);
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
        Instantiate(smallGreenScorpians, GetRandomSpawnVector(),Quaternion.identity);

    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
