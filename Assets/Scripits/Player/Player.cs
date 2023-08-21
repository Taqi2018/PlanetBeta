using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{


    [SerializeField] public float health;
    public HealthBar playerHealthBar;
    
    public float maxHealth;






    public static Player Instance { private set; get; }
    private bool isPlayerSelected;
   public bool isWalking;
    public bool isDieing;
    private Vector3 movementVector2d;
    Vector3 moveDir;
    [SerializeField] float playerSpeed, playerRotationSpeed;
    private bool isPlayerCollide;

    private void Awake()
    {

        Instance = this;
    }
    


    private void Start()
    {
        health = maxHealth;



     EventGenrator.Instance.OnPlayerWalking += ActionOnPlayerWalkingEvent;
        TargetRange.Instance.OnEnemyInTarget += ActionOnEnemyTargetEvent;
   

       isWalking = false;
       isPlayerSelected = false;
    }


    private void ActionOnPlayerWalkingEvent(object sender, EventGenrator.OnPlayerWalkingEventArgs e)
    {


        if (Vector2.Equals(e.inputVector, Vector2.zero))
        {
    
            isWalking = false;
        }
        else
        {

            isWalking = true;
            movementVector2d = e.inputVector;
            moveDir = new Vector3(movementVector2d.x, 0f, movementVector2d.y);
        }


    }


    void Update()
    {

        MovementHandler();

    }

    private void MovementHandler()
    {
         
       bool canMove = !IsPlayerCollide();


       if (canMove)
        {
            if (isWalking)
            {
                Walking();
            }
        }

    }


    private bool IsPlayerCollide()
    {
        float playerRadius = 0.3f;
        float playerHeight = 2.0f;
        float rangeToDetectCollision = playerSpeed * Time.deltaTime;
      
        bool isCollide = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir,out RaycastHit hit,rangeToDetectCollision);
      //  Debug.Log(hit.transform.name);
   /*     if(hit.transform.TryGetComponent(out MedPack m))
        {
            if (m != null)
            {
                m.HealthBooster();
                return false;
            }

        }*/


    
        if (isCollide)
        {
            //Attemp to move in X

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            isCollide = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, rangeToDetectCollision);
            if (!isCollide)
            {
                moveDir = moveDirX;
            }

            if (isCollide)
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                isCollide = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, rangeToDetectCollision);
                if (!isCollide)
                {
                    moveDir = moveDirZ;
                }
            }


        }
        return isCollide;
    }



    private void Walking()
    {

    

        transform.position += moveDir * Time.deltaTime * playerSpeed;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, playerRotationSpeed * Time.deltaTime);

    }


    public bool IsWalking()
    {
        return isWalking;
    }








    public void ActionOnPlayerSelected(object sender, EventArgs e)
    {
        isPlayerSelected = true;
    }




    private void ActionOnEnemyTargetEvent(object sender, TargetRange.OnEnemyInTargetEventArgs e)
    {

        // isWalking = false;
        if (!IsWalking())
        {
            Vector3 normalEnemeyPos = new Vector3(e.enemyPosition.x, 0, e.enemyPosition.z);
            transform.forward = Vector3.Slerp(transform.position, normalEnemeyPos - transform.position, playerRotationSpeed);
            
        }

    }


 




    public bool IsPlayerSelected()
    {
        return isPlayerSelected;
    }



}
