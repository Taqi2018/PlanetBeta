using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManger : MonoBehaviour
{

    public static InputManger Instance { get; private set; }

    public event EventHandler <onChangePositionEventArgs>TapEventInputActionRequired;




    Vector2 position;


   



    public class onChangePositionEventArgs : EventArgs
    {
       public Vector2 position;
    }

    

    private PlayerInputActions playerInputActions;
    private Vector2 inputVector2;


    [SerializeField] Transform player;
    [SerializeField]Camera cam;
    [SerializeField] float singleTapDelay;
    Vector3 currentPosition;
    private  bool isTap;
    private Vector2 tapPosition;
    private bool isHold;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();



    }

    private void Start()
    {
        ShootingController.Instance.OnSingleShootPerformedByPlayer += ActionOnSingleShoot;
        currentPosition = new Vector3(0, 0, 0);
    }

    private void ActionOnSingleShoot(object sender, EventArgs e)
    {


        isTap = false;

        
        
    }

    IEnumerator SingleTapDelay()
    {

        yield return new WaitForSeconds(singleTapDelay);

    }
/*
    //Only call once when we click mouse 
    public void MouseTriggerChangePositionEvent(InputAction.CallbackContext context)
    {



        if (context.started)
        {
            isTap = context.started;

            position = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
            TapEventInputActionRequired?.Invoke(this, new onChangePositionEventArgs { position = position });

        }

        if (context.performed)
        {

            isHold = context.performed;

        }
        if (context.canceled)
        {
            isHold = false;
        }


    }
*/

    public void TapTriggerChangePositionEvent(InputAction.CallbackContext context)
    {


        Debug.Log("hi");

        if (context.started)
        {
            isTap = context.started;


            tapPosition = playerInputActions.Player.TouchPosition.ReadValue<Vector2>();
            Debug.Log(tapPosition);
            TapEventInputActionRequired?.Invoke(this, new onChangePositionEventArgs { position = tapPosition });
      
        }

        if (context.performed)
        {

            isHold = context.performed;

        }
        if (context.canceled)
        {
            isHold = false;
        }


    }

















    public Vector2 GetInput()
    {
        inputVector2 = playerInputActions.Player.Move.ReadValue<Vector2>();
      




        return inputVector2;
    }

    public bool IsTap()
    {
        return isTap;
    }


    public bool IsHold()
    {
        return isHold;
    }



    private void Update()
    {
      

    }
}
