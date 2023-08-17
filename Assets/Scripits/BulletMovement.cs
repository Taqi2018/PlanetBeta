using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody bulletRigidBody;
    [SerializeField] float bulletSpeed;
    [SerializeField]public float damageOfWeapon;
    [SerializeField] ParticleSystem bulletHitShotGunEffect, bulletHitArEffect, bulletHitPistolEffect;

    public static BulletMovement Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
      bulletRigidBody=  transform.GetComponent<Rigidbody>();
  
        bulletRigidBody.velocity = ShootingController.Instance.shootDir * bulletSpeed;
     
    //    StartCoroutine(BulletDieTime());


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            Debug.Log(transform.name+"__");
            if (transform.name == "shotGunBullet(Clone)")
            {
                Debug.Log("shotGunEffect");
                Instantiate(bulletHitShotGunEffect, other.transform.position - Vector3.forward + Vector3.up, Quaternion.identity);
                bulletHitShotGunEffect.transform.gameObject.SetActive(true);
                bulletHitShotGunEffect.Play();
            }
            if (transform.name == "arBullet(Clone)")
            {
                Instantiate(bulletHitArEffect, other.transform.position - Vector3.forward + Vector3.up, Quaternion.identity);
                bulletHitArEffect.transform.gameObject.SetActive(true);
                bulletHitArEffect.Play();
            }
            if (transform.name == "pistolBullet(Clone)")
            {
                Instantiate(bulletHitPistolEffect, other.transform.position - Vector3.forward + Vector3.up, Quaternion.identity);
                bulletHitPistolEffect.transform.gameObject.SetActive(true);
                bulletHitPistolEffect.Play();
            }
            Debug.Log("bullet");
          
            Destroy(gameObject);

        }
        
/*        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null)
            {

                enemy.health = enemy.health - damageOfWeapon;
                enemy.HealthBar.SetHealthBar(enemy.health);
                if (enemy.health <= 0)
                {
                    if (enemy.name == "Scorpian" || enemy.name == "Scorpian(Clone)")
                    {
                        enemy.isScorpianDead = true;
                        // Destroy(enemy.gameObject);
                        enemy.StopEnemy();

                        StartCoroutine(DelayForDeathAnimation(enemy));
                    }
                    else
                    {
                        Destroy(enemy.gameObject);
                    }

*/


                }
            
}


/*
    IEnumerator DelayForDeathAnimation(Enemy e)
    {

        yield return new WaitForSeconds(e.animationDeathTime);
  

            Destroy(e.gameObject);
        
        Destroy(transform.gameObject);
    }
*/
/*
    IEnumerator BulletDieTime()
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(transform.gameObject);
    }*/
