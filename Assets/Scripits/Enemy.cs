using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public event EventHandler OnAttacKToPlayer;
    public static Enemy Instance { get; private set; }
    [SerializeField]NavMeshAgent navMeshAgent;
    
    private int killCounter;

    Vector3 moveDir;
    [SerializeField]private float attackingRangeOfScorpian;
    [SerializeField]private LayerMask playerLayerMask;
    [SerializeField]private float playerChasingRange;
    private bool isWalking;
    private bool isPlayerInChasingRangeOfScorpian;
    private bool isAttack;
    private Vector3 currentPosition;
    [SerializeField]private float attackDelay;
    [SerializeField]private float attackingRangeForShield;
    int totalShieldParts;
  


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        navMeshAgent = GetComponent<NavMeshAgent>();

        moveDir = Ship.Instance.transform.position- transform.position;
        transform.forward = Vector3.Slerp(transform.position, moveDir, 0.0001f);

        ShiedDestructionEvent.Instance.OnDestructionOfLastPart += GameOver;

        totalShieldParts = 31;
     


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
        }
        else
        {
            AttackToPlayer();         
        }

    }

    private void MoveTowardsPlayer()
    {
        isWalking = true;
        isAttack = false;


        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }


    private void AttackToPlayer()
    {
        if (!isAttack)
        {

            navMeshAgent.SetDestination(transform.position);
            isWalking = false;
            isAttack = true;
            ReduceHp();
            StartCoroutine(AttackDelay());
    
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttack = false;
    }



 


    private void ShipInteractionHandeling()
    {
        if (Vector3.Distance(transform.position, Ship.Instance.transform.position) > attackingRangeForShield)
        {
            isWalking = true;
            isAttack = false;
            navMeshAgent.SetDestination(Ship.Instance.transform.position);
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
       

        for (int i = 30; i >= 0; i--)
        {
            Debug.Log(i);
            Debug.Log(ShieldGrower.Instance.activeShieldParts[i]);
           
            GameObject shieldPart = ShieldGrower.Instance.activeShieldParts[i];
            if (shieldPart.activeInHierarchy)
            {
                shieldPart.SetActive(false);
                break;
            }


        }
     
        //   Debug.Log(ShieldGrower.Instance.shieldPartToActivate.name);
        /*ShieldGrower.Instance.shieldPartToActivate.gameObject.SetActive(false);*/
        Debug.Log("shield.....,,,,,,,,,,,,,,,,");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("--------");
        if (other.TryGetComponent(out BulletMovement bullet))
        {

            killCounter++;
        }
        if (killCounter ==7)
        {
            Destroy(transform.gameObject);
        }
    }



    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsAttacking()
    {
        return isAttack;
    }


  void ReduceHp()
    {


        Debug.Log("Hp Reduction!!");
        Player.Instance.hP = Player.Instance.hP - 10;

        GameOverCheck();

    }



    private void GameOverCheck()
    {
        if (Player.Instance.hP <= 0)
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");

            Destroy(transform.gameObject);
        }
    }

}
