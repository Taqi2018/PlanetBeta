using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody bulletRigidBody;
    [SerializeField] float bulletSpeed;
    [SerializeField]private float damageOfWeapon;

    // Start is called before the first frame update
    void Start()
    {

      bulletRigidBody=  transform.GetComponent<Rigidbody>();
        

      bulletRigidBody.velocity = ShootingController.Instance.shootDir * bulletSpeed;
        StartCoroutine(BulletDieTime());


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.health = enemy.health - damageOfWeapon;
            enemy.HealthBar.SetHealthBar(enemy.health);
            if (enemy.health <= 0)
            {
                Destroy(enemy.gameObject);
            }
            Destroy(transform.gameObject);
        }

    }




    IEnumerator BulletDieTime()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(transform.gameObject);
    }
 
}
