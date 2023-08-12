using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float enemyBulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody bulletRigidBody = transform.GetComponent<Rigidbody>();
        Vector3 shootDir = Player.Instance.transform.position - transform.position;

        bulletRigidBody.velocity = shootDir * enemyBulletSpeed*Time.deltaTime;


  

    }
    private void OnCollisionExit(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        
        if (other.transform.TryGetComponent(out Player player))
        {
            Debug.Log(other.transform.name);
            Player.Instance.health = Player.Instance.health - 10;
            player.playerHealthBar.SetHealthBar(Player.Instance.health);
       

        }
    }

    
    /*    private void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.transform.name);
            if(other.transform.TryGetComponent(out Player player))
            {
                Player.Instance.hP = Player.Instance.hP - 10;
                Debug.Log("attack................");
            }

        }*/
    // Update is called once per frame
    void Update()
    {
        
    }

}
