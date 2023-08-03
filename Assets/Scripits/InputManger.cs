using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManger : MonoBehaviour
{

    public static InputManger Instance { get; private set; }

    public event EventHandler <onChangePositionEventArgs>TapEventInputActionRequired;


    public event EventHandler HoldEventInputActionRequired;

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
    Vector3 previousPosition;
    private  bool isTap;
    private bool isHold;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //playerInputActions.Player.MouseClick.performed += _ => MouseTriggerChangePositionEvent();

        // playerInputActions.Player.MouseClick.started  += _ => MouseTriggerChangePositionEvent();
        //   playerInputActions.Player.MouseClick.performed += _ => MouseTriggerChangePositionEvent();
        // playerInputActions.Player.MouseClick.canceled += _ => MouseTriggerChangePositionEvent();
        // playerInputActions.Player.TapCheck.performed   += _ => TapTriggerChangePositionEvent();


    }

    private void Start()
    {
        ShootingController.Instance.OnSingleShootPerformedByPlayer += ActionOnSingleShoot;
    }

    private void ActionOnSingleShoot(object sender, EventArgs e)
    {


        isTap = false;
        //StartCoroutine(SingleTapDelay());
        
        
    }

    IEnumerator SingleTapDelay()
    {

        yield return new WaitForSeconds(singleTapDelay);
        //isTap = false;
    }

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
            HoldEventInputActionRequired?.Invoke(this, EventArgs.Empty);
        }
        if (context.canceled)
        {
            isHold = false;
        }


    }


    //Only call once when we click tap
    void TapTriggerChangePositionEvent()
    {
        position = playerInputActions.Player.TapPosition.ReadValue<Vector2>();
        TapEventInputActionRequired?.Invoke(this, new onChangePositionEventArgs { position = position });

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
