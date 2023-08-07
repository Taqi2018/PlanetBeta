using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform smallGreenScorpians;
    [SerializeField] Transform floor;
    [SerializeField]Transform drone;


    // Start is called before the first frame update
    void Start()
    {
       LoopTheScorpianSpawn();
        LoopTheDroneSpawn();
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

    private float VariationInSpawnPosition()
    {
        float variationFactor = UnityEngine.Random.Range(-2, +2);
        return variationFactor;

    }

    IEnumerator RandomScorpianSpawnTimer()
    {
        int randomScorpianSpwanTime = UnityEngine.Random.Range(5, 10);
        yield return new WaitForSeconds(randomScorpianSpwanTime);
        Debug.Log("Hi I am coming..");
        SpwanGreenScorpian();
        LoopTheScorpianSpawn();


    }

    private Vector3 GetRandomSpawnVector()
    {
        Vector3 randomSpawnPosition = CalculateRandomPositionVector();
        return randomSpawnPosition;
    }
    private void SpwanGreenScorpian()
    {
        Instantiate(smallGreenScorpians, GetRandomSpawnVector(),Quaternion.identity);

    }


    private Vector3 CalculateRandomPositionVector()
    {
        int xRandom = UnityEngine.Random.Range(0, Convert.ToInt32(floor.localScale.x));
        int zRandom = UnityEngine.Random.Range(0, Convert.ToInt32(floor.localScale.z));

        Vector3 randomSpawnPosition = new Vector3(xRandom, 0, zRandom);

        return randomSpawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
