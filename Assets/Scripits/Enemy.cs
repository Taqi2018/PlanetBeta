using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [SerializeField]NavMeshAgent navMeshAgent;
    
    private int killCounter;

    Vector3 moveDir;
    private float playerAttackingRange;
    private LayerMask playerLayerMask;
    private float playerChasingRange;
    private bool isPlayerInChasingRange;
    private bool isPlayerInAttackingRange;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        moveDir = Ship.Instance.transform.position- transform.position;
        transform.forward = Vector3.Slerp(transform.position, moveDir, 0.0001f);


    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInChasingRange = Physics.CheckSphere(transform.position, playerChasingRange, playerLayerMask);
        isPlayerInAttackingRange = Physics.CheckSphere(transform.position, playerAttackingRange, playerLayerMask);

        if(!isPlayerInAttackingRange && !isPlayerInChasingRange)
        {
         
            navMeshAgent.SetDestination(Ship.Instance.transform.position);
        }
        
  




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
