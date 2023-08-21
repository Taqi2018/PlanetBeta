using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManger : MonoBehaviour
{

    public static InputManger Instance { get; private set; }

    public event EventHandler <onChangePositionEventArgs>OnJoyStickMovement;

    public event EventHandler <OnTouchScreenEventArgs> OnTouchScreen;


    public class OnTouchScreenEventArgs : EventArgs
    {
        public Vector2 touchLocation;
    }





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

    //Only call once when we click mouse 
    public void TouchTriggerEvent(InputAction.CallbackContext context)
    {



        if (context.started)
        {


            position = playerInputActions.Player.TouchPosition.ReadValue<Vector2>();
            OnTouchScreen?.Invoke(this, new OnTouchScreenEventArgs { touchLocation = position });

        }



    }




    public void JoyStickMovementEvent(InputAction.CallbackContext context)
    {
        
           

        if (context.canceled)
        {
            OnJoyStickMovement?.Invoke(this, new onChangePositionEventArgs { position =Vector2.zero });

        }
        else
        {
            Vector2 movementVector = playerInputActions.Player.JoyStick.ReadValue<Vector2>();

            OnJoyStickMovement?.Invoke(this, new onChangePositionEventArgs { position = movementVector });
        }
        
     

    }

    public void TapTriggerChangePositionEvent(InputAction.CallbackContext context)
    {

/*
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

*/
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
