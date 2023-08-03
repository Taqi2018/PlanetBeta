using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int killCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BulletMovement bullet))
        {
            killCounter++;
        }
        if (killCounter == 3)
        {
            Destroy(transform.gameObject);
        }
    }
}
