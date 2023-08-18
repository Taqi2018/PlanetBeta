using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{


   public enum Guns
    {
        ShotGun,
        Ar,
        Pistol
    }

    public Guns guns;

    [SerializeField] public ParticleSystem bulletInstantiateParticles;
    public event EventHandler OnSingleShootPerformedByPlayer;

 


    public static  ShootingController Instance{get; private set;} 
    
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform arBullet,ShotGunBullet,pistolBulletPrefab;
    [SerializeField] public Transform ar, pistol, shotgun;
    [SerializeField] public float shotGunBulletAmount, arGunBulletAmount;
 
    private bool isShooting;
    [SerializeField] float shootingDelayTime;
    private bool isSingleMode, isBrustMode;


    public Vector3 shootDir;
    public  bool lockUpdate;
    public float fireDelay;

    private void Start()
    {
        
        guns = Guns.Pistol;
        pistol.gameObject.SetActive(true);
        ar.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        
        lockUpdate = false;
        Instance = this;
    /*    EventGenrator.Instance.OnEnemyTarget += SetShootDirection;*/
       EventGenrator.Instance.OnPlayerWalking += ActionOnPlayerWalking;
        TargetRange.Instance.OnEnemyInTarget   += SetShootDirection;
        TargetRange.Instance.OnNoEnemyInTarget += StopShootingEvent;

       // InputManger.Instance.OnTouchScreen += SelectGunEvent;

        OnSingleShootPerformedByPlayer += OffSingleShoot;

 
    }

/*    private void SelectGunEvent(object sender, InputManger.OnTouchScreenEventArgs e)
    {
        Camera.main.ScreenPointToRay()
    }
*/
    private void StopShootingEvent(object sender, EventArgs e)
    {

        Debug.Log("stop it fucker!!!");
        isShooting = false;
        isBrustMode = false;
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
        //isShooting = false;
        
      // isBrustMode = false;
    }

    private void SetShootDirection(object sender, TargetRange.OnEnemyInTargetEventArgs e)
    {
        isShooting = true;
        shootDir = e.enemyPosition - shootingPoint.position;

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
        if (isShooting   & !Player.Instance.IsWalking())
        {
           
            if (lockUpdate == false)
            {
                Debug.Log("Fire__");
                lockUpdate = true;
                StartCoroutine(Fire());
                Fire();
            }



        }
    }

    IEnumerator Fire()
    {
        isBrustMode = true;
        
        bulletInstantiateParticles.gameObject.SetActive(true);
        bulletInstantiateParticles.Play();

        if (guns == Guns.Pistol)
        {
            Debug.Log("Pistol");
            pistol.gameObject.SetActive(true);
/*            ar.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(false);
            ar.gameObject.SetActive(true);*/

            Instantiate(pistolBulletPrefab, shootingPoint.position, Quaternion.LookRotation(shootDir, Vector3.up));
          
            SoundManager.Instance.Play("pistol");
        }
        if (guns == Guns.Ar)
        {
            if (arGunBulletAmount > 0)
            {
                Debug.Log("Ar");
                /*            pistol.gameObject.SetActive(false);
                            ar.gameObject.SetActive(true);
                            shotgun.gameObject.SetActive(false);*/
                Instantiate(arBullet, shootingPoint.position, Quaternion.LookRotation(shootDir, Vector3.up));
                SoundManager.Instance.Play("ar");
                GunSelectionUi.Instance.ArSlider();
                arGunBulletAmount--;
            }
            else
            {
                SoundManager.Instance.Play("empty");


            }
        
        }
        if (guns == Guns.ShotGun)
        {
            if (shotGunBulletAmount > 0)
            {
                Debug.Log("sg");
                /*            pistol.gameObject.SetActive(false);
                            ar.gameObject.SetActive(false);
                            shotgun.gameObject.SetActive(true);*/
                Instantiate(ShotGunBullet, shootingPoint.position, Quaternion.LookRotation(shootDir, Vector3.up));
                SoundManager.Instance.Play("shootGun");
                GunSelectionUi.Instance.ShotGunSlider();
                shotGunBulletAmount--;
            }
            else
            {
                SoundManager.Instance.Play("empty");
               
            }
         
        }
        //  Instantiate(bullerPrefab, shootingPoint.position, Quaternion.LookRotation(shootDir, Vector3.up));
       // SoundManager.Instance.Play("ar");
        yield return new WaitForSeconds(fireDelay);
        lockUpdate = false;
    }
}
