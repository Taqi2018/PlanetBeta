using System;
using UnityEngine;

public class Player : MonoBehaviour
{


  

    public static Player Instance { private set; get; }
    private bool isPlayerSelected;
    private bool isWalking;
    private Vector3 targetPosition;

    [SerializeField] float playerSpeed, playerRotationSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

     EventGenrator.Instance.OnPlayerSelected += ActionOnPlayerSelected;
     EventGenrator.Instance.OnPlayerWalking += ActionOnPlayerWalkingEvent;
     //
       EventGenrator.Instance.OnEnemyTarget += ActionOnEnemyTargetEvent;

       isWalking = false;
       isPlayerSelected = false;
    }



    public void ActionOnPlayerSelected(object sender, EventArgs e)
    {
        Debug.Log("playerhi00t");
        isPlayerSelected = true;
    }


    private void ActionOnPlayerWalkingEvent(object sender, EventGenrator.OnPlayerWalkingEventArgs e)
    {
        isWalking = true;
        targetPosition = e.PlayerWalkToPoint;
    }


    private void ActionOnEnemyTargetEvent(object sender, EventGenrator.OnEnemyTargetEventArgs e)
    {
        isWalking = false;

            transform.forward = Vector3.Slerp(transform.position, e.OnEnemyTargetPoint - transform.position, playerRotationSpeed);

    }


 

    void Update()
    {

        MovementHandler();

    }



    private void MovementHandler()
    {
        if (isWalking)
        {
            WalkToTargetPosition();
        }

    }

    private void WalkToTargetPosition()
    {
    
        Vector3 playerDirection = targetPosition - transform.position;
        transform.forward = Vector3.Slerp(transform.forward, playerDirection, playerRotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerSpeed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isWalking = false;
        }
    }


    public bool IsWalking()
    {
        return isWalking;
    }


    public bool IsPlayerSelected()
    {
        return isPlayerSelected;
    }
}
