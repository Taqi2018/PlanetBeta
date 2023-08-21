using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using System.Collections.Generic;

using System.Linq;

using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{

    public event EventHandler OnGameOver;
    public static Enemy Instance { get; private set; }
    public bool PlayerOnTarget { get; private set; }
    public ParticleSystem bloodParticles;

    [SerializeField] NavMeshAgent navMeshAgent;

    private int killCounter;

    Vector3 moveDir;
    [SerializeField] private float attackingRangeOfScorpian;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float playerChasingRange;
    private bool isWalking;
    private bool isPlayerInChasingRangeOfScorpian;
    private bool isAttack;
    private Vector3 currentPosition;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackingRangeForShield;
    GameObject[] ships;
 
    private GameObject shipSelectedToAttack;
    [SerializeField] private Transform alienShootingPoint;
    [SerializeField] Transform enemyBulletPrefab;
    [SerializeField] private float enemyBulletSpeed;
    public float maxHealth;
    public float health;
    public HealthBar HealthBar;
    public Vector3 shootDir;

    List<float> Distance;
    public bool isScorpianDead;
    public bool isAlienDead;
    public bool isDroneDead;
    public ParticleSystem droneHitEffect;
    public float animationDeathTime;
    public float scorpianDamge;
    private bool scorpianAttacking;
    private int onlyAttackShip;
    public bool enemyDead;

    public ParticleSystem alienSpawnEffect;



    // Start is called before the first frame update
    void Start()
    {

        Distance = new List<float>();
      
        health = maxHealth;


        Instance = this;
        navMeshAgent = GetComponent<NavMeshAgent>();
        LoadShips();
       
        moveDir = Ship.Instance.transform.position - transform.position;
        transform.forward = Vector3.Slerp(transform.position, moveDir, 0.0001f);
        onlyAttackShip = UnityEngine.Random.Range(0, 2);
        Debug.Log(onlyAttackShip + " -->s");
        // ShiedDestructionEvent.Instance.OnDestructionOfLastPart += GameOver;



        int a = ships.Length;
        for (int i = 0; i < a; i++)
        {
            Distance.Add(Vector3.Distance(transform.position, ships[i].transform.position));
        }

        int minValue = FindMinValueIndex(Distance);


       

        shipSelectedToAttack = ships[minValue];

        if (transform.name == "Alien(Clone)")
        {
            alienSpawnEffect.transform.gameObject.SetActive(true);
            Instantiate(alienSpawnEffect, transform.position + Vector3.down * 5, Quaternion.identity);
            alienSpawnEffect.Play();
        }
    }





    int FindMinValueIndex(List<float> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new System.ArgumentException("List is empty or null.");
        }

        int minValueIndex = 0;  // Assume the minimum value is at index 0
        float minValue = list[0];

        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] < minValue)
            {
                minValue = list[i];
                minValueIndex = i;  // Update the index of the minimum value
            }
        }

        return minValueIndex;
    }







    private void LoadShips()
    {
        ships = GameObject.FindGameObjectsWithTag("zPoint");
        Debug.Log(ships[0].transform.parent.GetChild(0).name +" hiiiiiiiiiiiii");
     //   Debug.Log(ships[1].transform.parent.GetChild(0).name + " hellllllllo");



    }

/*    private void GameOver(object sender, EventArgs e)
    {
        Debug.Log("gameovere");
        //      SceneManager.LoadScene("GameOver");
    }
*/
    // Update is called once per frame
    void Update()
    {
        if (transform.name == "Scorpian" || transform.name == "Scorpian(Clone)")
        {
           
            // attack only ship
            if (onlyAttackShip == 1)
            {
                if (health < health/2)
                {
                    onlyAttackShip = 0;
                }
        
                ShipInteractionHandeling();
            }

            //attack both ship and player
            if (onlyAttackShip == 0)
            {
                if (!isScorpianDead)
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
            }
        }
        if(transform.name == "Alien" || transform.name == "Alien(Clone)")
        {
            if (!isAlienDead)
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

        }


        if (transform.name == "Drone" || transform.name == "Drone(Clone)")
        {
            if (!isDroneDead)
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
        if (!isAttack && transform.name == "Scorpian" || transform.name == "Scorpian(Clone)")
        {

            isWalking = false;
            isAttack = true;



            transform.forward = Vector3.Slerp(transform.forward, Player.Instance.transform.position - transform.position, 2.0f);
         
            if (!scorpianAttacking)
            {
                scorpianAttacking = true;
                Player.Instance.health -= scorpianDamge;
                Player.Instance.playerHealthBar.SetHealthBar(Player.Instance.health);
                GameOverCheckForPlayer();
                AttackDelay();
            }


        }
        else if (!isAttack)
        {

            
            AttackPlayer();
            //  StartCoroutine(AttackDelay());

        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        if (transform.name == "Scorpian(Clone)")
        {
            scorpianAttacking = false;
        }
        //   isAttack = false;
    }

    void AttackPlayer()
    {
        //stop


        isWalking = false;
        isAttack = true;



        transform.forward = Vector3.Slerp(transform.forward, Player.Instance.transform.position - transform.position, 2.0f);

        StartCoroutine(AllienShootingDelay());






        GameOverCheckForPlayer();

    }

    private IEnumerator AllienShootingDelay()
    {

        yield return new WaitForSeconds(UnityEngine.Random.Range(1, 5));
        Transform bullet = Instantiate(enemyBulletPrefab, alienShootingPoint.position, Quaternion.LookRotation(Player.Instance.transform.position - transform.position, Vector3.up));
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
        if (Vector3.Distance(transform.position, shipSelectedToAttack.transform.position) > attackingRangeForShield)
        {
            isWalking = true;
            isAttack = false;





            int randomNo = UnityEngine.Random.Range(0, 2);





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


    public void StopEnemy()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    private void ReduceShipHp()
    {


        if (transform.name == "Scorpian" || transform.name == "Scorpian(Clone)")
        {

            for (int i = 30; i >= 0; i--)
            {
                Debug.Log(shipSelectedToAttack.transform.parent.GetChild(0).name);

                shipSelectedToAttack.transform.parent.TryGetComponent(out ShieldGrower s);
               GameObject shieldPart= s.activeShieldParts[i];
                if (shieldPart.activeInHierarchy)
                {
                    if (shieldPart.name == "Part1")
                    {
                        GameOverByShieldDestruction();
                    }
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


    private void GameOverByShieldDestruction()
    {
        StartCoroutine(DieAnimationDelay());
    }




    private void GameOverCheckForPlayer()
    {
        if (Player.Instance.health <= 0)
        {
            Player.Instance.isDieing = true;
            Debug.Log("GameOver");
            StartCoroutine(DieAnimationDelay());
        }
    }

    private IEnumerator DieAnimationDelay()
    {
        ShootingController.Instance.enabled = false;
        
        Player.Instance.isWalking = false;
        ShootingController.Instance.isShooting = false;
        ShootingController.Instance.lockUpdate = true;
        yield return new WaitForSeconds(3.0f);
  
        GameplayUIManager.Instance.LevelFailedPanel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BulletMovement b))
        {
            health = health - b.damageOfWeapon;
            HealthBar.SetHealthBar(health);
            if (health <= 0)
            {
                if (name == "Scorpian" || name == "Scorpian(Clone)")
                {
                    isScorpianDead = true;
                    transform.GetComponent<BoxCollider>().enabled = false;
                    enemyDead = true;
                    // Destroy(enemy.gameObject);
                    StopEnemy();

                    StartCoroutine(DelayForDeathAnimation());
                }
                if (name == "Alien" || name == "Alien(Clone)")
                {
                    isAlienDead = true;
                    // Destroy(enemy.gameObject);
                    StopEnemy();

                    StartCoroutine(DelayForDeathAnimation());
                }

                if (name == "Drone" || name == "Drone(Clone)")
                {
                    isDroneDead = true;

                   // Instantiate(droneHitEffect, transform.position, Quaternion.identity);
                    droneHitEffect.transform.gameObject.SetActive(true);
                    droneHitEffect.Play();
                   // transform.position= Vector3.MoveTowards(other.transform.position, (Vector3.down - other.transform.position)*5,2);
                    // Destroy(enemy.gameObject);
                    StopEnemy();

                    StartCoroutine(DelayForDeathAnimation());
                }



            }

            bloodParticles.transform.gameObject.SetActive(true);
            bloodParticles.Play();


        }

        IEnumerator DelayForDeathAnimation()
        {
            TargetRange.Instance.enemies.Remove(transform.gameObject);


            yield return new WaitForSeconds(animationDeathTime);

            if(transform.name=="Drone" || transform.name == "Drone(Clone)")
            {
                Debug.Log("dsaojdoasjdpoasjdpoasjdoasjdpoasjdoa");
             droneHitEffect.transform.gameObject.SetActive(false);
            }

            Destroy(transform.gameObject);

           // Destroy(transform.gameObject);
        }
    }
}