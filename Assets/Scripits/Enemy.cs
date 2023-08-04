using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    private int killCounter;
    [SerializeField] Transform Ship;
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        moveDir = Ship.transform.position - transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Vector3.Slerp(transform.position, moveDir, 4.0f);


        transform.position +=  moveDir *Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("--------");
        if (other.TryGetComponent(out BulletMovement bullet))
        {
            Debug.Log("CollideWithBullet");
            killCounter++;
        }
        if (killCounter ==7)
        {
            Destroy(transform.gameObject);
        }
    }
}
