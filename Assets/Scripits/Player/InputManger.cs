using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    private int introTouches;

    public Image messanger;
    public TextMeshProUGUI intro;
    public TextMeshProUGUI news;
    public TextMeshProUGUI command;


    private void Awake()
    {
       
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();



    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            
            Time.timeScale = 0;
            messanger.gameObject.SetActive(true);
            intro.gameObject.SetActive(true);


        }
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


        Debug.Log("Toch!!");
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
        if (context.started)
        {
            //StartCoroutine(DelayForInstruction());
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                introTouches++;
                if (introTouches == 1)
                {
                 
                    
                    intro.gameObject.SetActive(false);
                    messanger.gameObject.SetActive(false);


                    messanger.gameObject.SetActive(true);
                    news.gameObject.SetActive(true);
                    //text disable 1
                    //image disable 

                    //image enable 
                    //text enable 2

                    Debug.Log("Toch!!");
                }

                if (introTouches == 2)
                {


                    news.gameObject.SetActive(false);
                    messanger.gameObject.SetActive(false);


                    messanger.gameObject.SetActive(true);
                    command.gameObject.SetActive(true);
                    //text disable 2
                    //image disable 

                    //image enable 
                    //text enable 3

                    Debug.Log("Toch!!");
                }
                if (introTouches == 3)
                {
                    messanger.gameObject.SetActive(false);
                    command.gameObject.SetActive(false);
                    //text disable 1
                    //image disable 
                    GameplayUIManager.Instance.joystickCanvaus.gameObject.SetActive(true);
                    GameplayUIManager.Instance.gunPanel.gameObject.SetActive(true);
                    Time.timeScale = 1;

                    Debug.Log("Toch!!");
                }
                if (Player.Instance.oxygenInstruction.IsActive())
                {
                    GameplayUIManager.Instance.joystickCanvaus.gameObject.SetActive(true);
                    GameplayUIManager.Instance.gunPanel.gameObject.SetActive(true);
                    Player.Instance.oxygenInstruction.gameObject.SetActive(false);
                    Player.Instance.messanger.gameObject.SetActive(false);
                    Time.timeScale = 1;
                }

                if (Player.Instance.healthInstruction.IsActive())
                {
                    GameplayUIManager.Instance.joystickCanvaus.gameObject.SetActive(true);
                    GameplayUIManager.Instance.gunPanel.gameObject.SetActive(true);
                    Player.Instance.healthInstruction.gameObject.SetActive(false);
                    Player.Instance.messanger.gameObject.SetActive(false);
                    Time.timeScale = 1;
                }

       
            }
            if (SceneManager.GetActiveScene().name == "Level3")
            {

                if (Player.Instance.ammoInstruction.IsActive())
                {

                    Player.Instance.ammoInstruction.gameObject.SetActive(false);
                    Player.Instance.messanger.gameObject.SetActive(false);
                    //GameplayUIManager.Instance.joystickCanvaus.gameObject.SetActive(true);
                    GameplayUIManager.Instance.gunPanel.gameObject.SetActive(true);
                    Time.timeScale = 1;
                }
            }
        }

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

    private IEnumerator DelayForInstruction()
    {
        yield return new WaitForSeconds(1.0f);
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
