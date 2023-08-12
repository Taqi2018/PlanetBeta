using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenrator : MonoBehaviour
{
    public static EventGenrator Instance { get; private set; }
    public event EventHandler OnPlayerSelected;

    public event EventHandler <OnPlayerWalkingEventArgs>OnPlayerWalking;
    public class OnPlayerWalkingEventArgs : EventArgs
    {
        public Vector2 inputVector;
    }

    public event EventHandler <OnEnemyTargetEventArgs>OnEnemyTarget;

    public class OnEnemyTargetEventArgs: EventArgs
    {
        public Vector3 OnEnemyTargetPoint;
    }

    void Start()
    {
        Instance = this;
        InputManger.Instance.OnJoyStickMovement += MovementByJoystickMethod;
    }

    private void MovementByJoystickMethod(object sender, InputManger.onChangePositionEventArgs e)
    {


        OnPlayerWalking?.Invoke(this, new OnPlayerWalkingEventArgs { inputVector = e.position });
     
    }

}
