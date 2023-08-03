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
        public Vector3 PlayerWalkToPoint;
    }

    public event EventHandler <OnEnemyTargetEventArgs>OnEnemyTarget;

    public class OnEnemyTargetEventArgs: EventArgs
    {
        public Vector3 OnEnemyTargetPoint;
    }

    void Start()
    {
        Instance = this;
        InputManger.Instance.TapEventInputActionRequired += EventGenratorMethod;
    }

    private void EventGenratorMethod(object sender, InputManger.onChangePositionEventArgs e)
    {

        
        Ray ray = Camera.main.ScreenPointToRay(e.position);


        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.TryGetComponent(out Player player))
            {

                OnPlayerSelected?.Invoke(this, EventArgs.Empty);

             

            }

            if (Player.Instance.IsPlayerSelected() & raycastHit.transform.name == "Floor")
            {
                OnPlayerWalking?.Invoke(this, new OnPlayerWalkingEventArgs {PlayerWalkToPoint =raycastHit.point});


            }
            if (Player.Instance.IsPlayerSelected() & raycastHit.transform.TryGetComponent(out Enemy enemy))
            {
                OnEnemyTarget?.Invoke(this, new OnEnemyTargetEventArgs { OnEnemyTargetPoint=raycastHit.point});



            }

        }

    }

}
