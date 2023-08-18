using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner: MonoBehaviour
{




    [SerializeField] private Transform ammoPrefab, medPackPrefab;
    [SerializeField] private float spawnTimer;
    List<Transform> spawnObjects;
    void Start()
    {
        spawnObjects = new List<Transform>();
        spawnObjects.Add(ammoPrefab);
        spawnObjects.Add(medPackPrefab);

      



        StartCoroutine(Spawn());
        
    }

    IEnumerator Spawn()
    {
        int random = Random.Range(0, spawnObjects.Count+1);
        yield return new WaitForSeconds(spawnTimer);
        Instantiate(spawnObjects[random], transform.position, Quaternion.identity);
    }

   
}










