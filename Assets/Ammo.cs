using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
        
    }

    public void AmmoBooster()
    {

        ShootingController.Instance.shotGunBulletAmount = GunSelectionUi.Instance.shotGun.maxValue;
        ShootingController.Instance.arGunBulletAmount = GunSelectionUi.Instance.ar.maxValue;
        GunSelectionUi.Instance.ar.value = 0;
        GunSelectionUi.Instance.shotGun.value = 0;
        Destroy(transform.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            AmmoBooster();

        }
    }
}
