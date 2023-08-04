using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform smallGreenScorpians;
    [SerializeField] Transform floor;


    // Start is called before the first frame update
    void Start()
    {
        LoopTheSpawn();
    }

    private void LoopTheSpawn()
    {
        StartCoroutine(RandomSpawnTimer());

    }



    IEnumerator RandomSpawnTimer()
    {
        int randomSpwanTime = UnityEngine.Random.Range(10, 15);
        yield return new WaitForSeconds(randomSpwanTime);
        Debug.Log("Hi I am coming..");
        SpwanGreenScorpian();
        LoopTheSpawn();


    }

    private void SpwanGreenScorpian()
    {
        Vector3 randomSpawnPosition = CalculateRandomPositionVector();
        Instantiate(smallGreenScorpians, randomSpawnPosition,Quaternion.identity);

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
