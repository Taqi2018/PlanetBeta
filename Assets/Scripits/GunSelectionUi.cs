using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunSelectionUi : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShotGun()
    {

        ShootingController.Instance.guns = ShootingController.Guns.ShotGun;
        ShootingController.Instance.pistol.gameObject.SetActive(false);
        ShootingController.Instance.shotgun.gameObject.SetActive(true);
        ShootingController.Instance.ar.gameObject.SetActive(false);
    }
    public void LaserGun()
    {

        ShootingController.Instance.guns = ShootingController.Guns.Ar;
        ShootingController.Instance.pistol.gameObject.SetActive(false);
        ShootingController.Instance.shotgun.gameObject.SetActive(false);
        ShootingController.Instance.ar.gameObject.SetActive(true);

    }
    public void Pistol()
    {
        
        ShootingController.Instance.guns = ShootingController.Guns.Pistol;
        ShootingController.Instance.pistol.gameObject.SetActive(true);
        ShootingController.Instance.shotgun.gameObject.SetActive(false);
        ShootingController.Instance.ar.gameObject.SetActive(false);
    }

}
