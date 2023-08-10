using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    public event EventHandler OnSingleShootPerformedByPlayer;

 


    public static  ShootingController Instance{get; private set;} 
    
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform bullerPrefab;
    private bool isShooting;
    [SerializeField] float shootingDelayTime;
    private bool isSingleMode, isBrustMode;


    public Vector3 shootDir;





    private void Start()
    {
        Instance = this;
        EventGenrator.Instance.OnEnemyTarget += SetShootDirection;
        EventGenrator.Instance.OnPlayerWalking += ActionOnPlayerWalking;

        OnSingleShootPerformedByPlayer += OffSingleShoot;

 
    }

    private void OffSingleShoot(object sender, EventArgs e)
    {
        StartCoroutine(SingleShotDeadTime());
    }


    IEnumerator SingleShotDeadTime()
    {
        yield return new WaitForSeconds(1.0f);
        isSingleMode = false;
    }

    private void ActionOnPlayerWalking(object sender, EventGenrator.OnPlayerWalkingEventArgs e)
    {
        isShooting = false;
    }

    private void SetShootDirection(object sender, EventGenrator.OnEnemyTargetEventArgs e)
    {
        isShooting = true;
        shootDir = e.OnEnemyTargetPoint - shootingPoint.position;
    }


    public bool IsShooting()
    {
        return isShooting;
    }



    public bool IsSingleMode()
    {
        return isSingleMode;
    }
    public bool IsBrustMode()
    {
        return isBrustMode;
    }



    private void Update()
    {
        if (isShooting)
        {
            if (InputManger.Instance.IsTap())
            {


                isSingleMode = true;
                isBrustMode = false;

                
                
                Instantiate(bullerPrefab, shootingPoint.position, Quaternion.LookRotation(shootDir, Vector3.up));
          

                OnSingleShootPerformedByPlayer.Invoke(this, EventArgs.Empty);

            }
            if (InputManger.Instance.IsHold())
            {

                isBrustMode = true;
                //isSingleMode = false;
                Instantiate(bullerPrefab, shootingPoint.position, Quaternion.LookRotation(shootDir,Vector3.up) );
        

                
            }
            if (!(InputManger.Instance.IsHold())){
                isBrustMode = false;
             
            }



        }
    }


    

}
