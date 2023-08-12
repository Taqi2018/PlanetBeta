using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] public float hP;






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
    // Enemy.Instance.OnAttacKToPlayer += ReduceHp;

     EventGenrator.Instance.OnPlayerSelected += ActionOnPlayerSelected;
     EventGenrator.Instance.OnPlayerWalking += ActionOnPlayerWalkingEvent;
     //
       EventGenrator.Instance.OnEnemyTarget += ActionOnEnemyTargetEvent;

       isWalking = false;
       isPlayerSelected = false;
    }

    private void ReduceHp(object sender, EventArgs e)
    {


        Debug.Log("Hp Reduction!!");
        hP = hP - 10;

        GameOverCheck();

    }

   


 



    private void GameOverCheck()
    {
        if (hP <= 0)
        {
            Debug.Log("GameOver");
            Destroy(transform.gameObject);
        }
    }


    public void ActionOnPlayerSelected(object sender, EventArgs e)
    {
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
          SoundManager.PlaySound(SoundManager.Sound.PlayerAttack);

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
          SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
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
