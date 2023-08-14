using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using System.Collections.Generic;



using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
 
    public static Enemy Instance { get; private set; }
    public bool PlayerOnTarget { get; private set; }
    public ParticleSystem bloodParticles;

    [SerializeField]NavMeshAgent navMeshAgent;
    
    private int killCounter;

    Vector3 moveDir;
    [SerializeField]private float attackingRangeOfScorpian;
    [SerializeField]private LayerMask playerLayerMask;
    [SerializeField]private float playerChasingRange;
    private bool isWalking;
    private bool isPlayerInChasingRangeOfScorpian;
    private  bool isAttack;
    private Vector3 currentPosition;
    [SerializeField]private float attackDelay;
    [SerializeField]private float attackingRangeForShield;
    GameObject[] ships;
    private GameObject shipSelectedToAttack;
    [SerializeField]private Transform alienShootingPoint;
    [SerializeField]  Transform enemyBulletPrefab;
    [SerializeField]private float enemyBulletSpeed;
    public float maxHealth;
    public  float health;
    public HealthBar HealthBar;
    public Vector3 shootDir;
    private bool hit;
    List <float> Distance;






    // Start is called before the first frame update
    void Start()
    {
 
        Distance = new List<float>();
        hit = false;
        health = maxHealth;
  
        
        Instance = this;
        navMeshAgent = GetComponent<NavMeshAgent>();
        LoadShields();
        moveDir = Ship.Instance.transform.position- transform.position;
        transform.forward = Vector3.Slerp(transform.position, moveDir, 0.0001f);

        ShiedDestructionEvent.Instance.OnDestructionOfLastPart += GameOver;



        int a = ships.Length;
        for(int i = 0; i <a; i++)
        {
            Distance.Add(Vector3.Distance(transform.position, ships[i].transform.position));
        }

   
        foreach(float g in Distance)
        {
            Debug.Log(g);
        }
        shipSelectedToAttack = ships[0];
        /*        float Distance= Vector3.Distance(transform.position, ships[0].transform.position);
                float Distance = Vector3.Distance(transform.position, ships[1].transform.position);
             //   float Distance3 = Vector3.Distance(transform.position, ships[2].transform.position);*/

        /*    if(Distance1>Distance2 & Distance1 > Distance3)
            {
                shipSelectedToAttack = ships[0];
            }
            if (Distance2 > Distance1 & Distance2 > Distance3)
            {
                shipSelectedToAttack = ships[1];
            }
            if (Distance3 > Distance1 & Distance3 > Distance2)
            {
                shipSelectedToAttack = ships[2];
            }*/

    }

    private void LoadShields()
    {
       ships = GameObject.FindGameObjectsWithTag("zPoint");
        foreach(GameObject s in ships)
        {
            Debug.Log(s.name);
        }


    }

    private void GameOver(object sender, EventArgs e)
    {
        Debug.Log("gameovere");
        //      SceneManager.LoadScene("GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInChasingRangeOfScorpian = Physics.CheckSphere(transform.position, playerChasingRange, playerLayerMask);



        if (!isPlayerInChasingRangeOfScorpian)
        {
            ShipInteractionHandeling();

        }




        if (isPlayerInChasingRangeOfScorpian)
        {
            PlayerInteractonHandeling();

        }






    }








    private void PlayerInteractonHandeling()
    {

       
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) > attackingRangeOfScorpian)
        {
            MoveTowardsPlayer();
            PlayerOnTarget = false;
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
            if (!PlayerOnTarget)
            {
                PlayerOnTarget = true;
                ManageAttackToPlayer();
            }
        }

    }

    private void MoveTowardsPlayer()
    {
      
        isWalking = true;
    //    isAttack = false;


        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }


    private void ManageAttackToPlayer()
    {

        if (!isAttack)
        {
         
        
            AttackPlayer();
          //  StartCoroutine(AttackDelay());
    
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
     //   isAttack = false;
    }

    void AttackPlayer()
    {
        //stop
     

        isWalking = false;
        isAttack = true;



        transform.forward = Vector3.Slerp(transform.forward, Player.Instance.transform.position - transform.position, 2.0f);
       
        StartCoroutine(AllienShootingDelay());
 





        GameOverCheck();

    }

    private IEnumerator AllienShootingDelay()
    {
        
        yield return new WaitForSeconds(UnityEngine.Random.Range(1, 5));
        Transform bullet = Instantiate(enemyBulletPrefab, alienShootingPoint.position, Quaternion.LookRotation(Player.Instance.transform.position-transform.position, Vector3.up));
        /*
                Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
                bulletRigidBody.velocity = shootDir * enemyBulletSpeed;
                */
        isAttack = false;
        yield return new WaitForSeconds(2.0f);
        ManageAttackToPlayer();

    

       // StartCoroutine(BulletDieTime(bullet));

    }
/*
    IEnumerator BulletDieTime(Transform bullet)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(bullet.gameObject);
    }*/




 


    private void ShipInteractionHandeling()
    {
        if (Vector3.Distance(transform.position, Ship.Instance.transform.position) > attackingRangeForShield)
        {
            isWalking = true;
            isAttack = false;



   

            int randomNo = UnityEngine.Random.Range(0,2);




          
            navMeshAgent.SetDestination(shipSelectedToAttack.transform.position);
        }
        else
        {
            AttackToShield();




        }

    }
    private void AttackToShield()
    {
        if (!isAttack)
        {

            navMeshAgent.SetDestination(transform.position);
            isWalking = false;
            isAttack = true;
            ReduceShipHp();
            StartCoroutine(AttackDelay());

        }
    }

    private void ReduceShipHp()
    {


        if (transform.name == "Scorpian"  || transform.name == "Scorpian(Clone)")
        {
  
            for (int i = 30; i >= 0; i--)
            {


                GameObject shieldPart = ShieldGrower.Instance.activeShieldParts[i];
                if (shieldPart.activeInHierarchy)
                {
                    shieldPart.SetActive(false);
                    break;
                }


            }

        }

        //   Debug.Log(ShieldGrower.Instance.shieldPartToActivate.name);
        /*ShieldGrower.Instance.shieldPartToActivate.gameObject.SetActive(false);*/

    }
/*
    private void OnTriggerEnter(Collider other)
    {
  
        if (other.TryGetComponent(out BulletMovement bullet))
        {

            killCounter++;
        }
        if (killCounter ==20)
        {
            Destroy(transform.gameObject);
        }
    }

*/

    public bool IsWalking()
    {
        return isWalking;
     //   return isWalking;
    }

    public bool IsAttacking()
    {
        return isAttack;
    }





    private void GameOverCheck()
    {
        if (Player.Instance.health <= 0)
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");

            Destroy(transform.gameObject);
        }
    }





    private void OnTriggerEnter(Collider other)
    {


        bloodParticles.transform.gameObject.SetActive(true);
            bloodParticles.Play();
          
 
    }
}
