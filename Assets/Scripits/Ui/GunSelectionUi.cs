using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunSelectionUi : MonoBehaviour
{
    public static GunSelectionUi Instance;
    public Slider ar;
    public Slider shotGun;
    public Button shotGunButton, laserGunButton, PistolButton;
    private Color  oColor;
    // Start is called before the first frame update
    void Start()
    {
        PistolButton.image.color = Color.red;

        oColor = shotGunButton.image.color;
        ar.value = 0;
        shotGun.value = 0;
        ar.maxValue = ShootingController.Instance.arGunBulletAmount;
        shotGun.maxValue = ShootingController.Instance.shotGunBulletAmount;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
/*        ar.value = 0;
        shotGun.value = 0;
        ar.maxValue = ShootingController.Instance.arGunBulletAmount;
        shotGun.minValue = ShootingController.Instance.shotGunBulletAmount;*/
    }
    public void ShotGun()
    {
        PistolButton.image.color = oColor;
        laserGunButton.image.color = oColor;
        shotGunButton.image.color = Color.red;
        ShootingController.Instance.guns = ShootingController.Guns.ShotGun;
        ShootingController.Instance.pistol.gameObject.SetActive(false);
        ShootingController.Instance.shotgun.gameObject.SetActive(true);
        ShootingController.Instance.ar.gameObject.SetActive(false);
     //   shotGun.value++;
    }
    public void LaserGun()
    {

        PistolButton.image.color = oColor;
        laserGunButton.image.color = Color.red;
        shotGunButton.image.color = oColor;
        ShootingController.Instance.guns = ShootingController.Guns.Ar;
        ShootingController.Instance.pistol.gameObject.SetActive(false);
        ShootingController.Instance.shotgun.gameObject.SetActive(false);
        ShootingController.Instance.ar.gameObject.SetActive(true);
     //   ar.value++;

    }
    public void Pistol()
    {
        PistolButton.image.color = Color.red;
        laserGunButton.image.color = oColor;
        shotGunButton.image.color = oColor;
        ShootingController.Instance.guns = ShootingController.Guns.Pistol;
        ShootingController.Instance.pistol.gameObject.SetActive(true);
        ShootingController.Instance.shotgun.gameObject.SetActive(false);
        ShootingController.Instance.ar.gameObject.SetActive(false);
       
    }

    public void ShotGunSlider()
    {

        shotGun.value++;
    }

    public void ArSlider()
    {
        ar.value++;
    }
}
