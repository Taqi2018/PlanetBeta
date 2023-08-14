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
    private bool isWalking;
    private Vector3 movementVector2d;

    [SerializeField] float playerSpeed, playerRotationSpeed;

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
            Debug.Log(e.inputVector);
            isWalking = false;
        }
        else
        {

            isWalking = true;
            movementVector2d = e.inputVector;
        }


    }


    void Update()
    {

        MovementHandler();

    }

    private void MovementHandler()
    {
      

        if (isWalking)
        {
            Walking();
        }

    }


  
    private void Walking()
    {

        Vector3 playerMovement3dVec = new Vector3(movementVector2d.x, 0f, movementVector2d.y);

        transform.position += playerMovement3dVec * Time.deltaTime * playerSpeed;

        transform.forward = Vector3.Slerp(transform.forward, playerMovement3dVec, playerRotationSpeed * Time.deltaTime);

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
        isWalking = false;

        transform.forward = Vector3.Slerp(transform.position, e.enemyPosition - transform.position, playerRotationSpeed);

    }


 




    public bool IsPlayerSelected()
    {
        return isPlayerSelected;
    }



}
