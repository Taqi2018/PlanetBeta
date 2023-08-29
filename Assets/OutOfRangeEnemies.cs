using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfRangeEnemies : MonoBehaviour
{
    public List<GameObject> outOfRangeEnemies;
    // Start is called before the first frame update
    void Start()
    {
        outOfRangeEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Enemy e))
        {
            outOfRangeEnemies.Add(e.gameObject);
        }
    }
}
