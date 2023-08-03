using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody bulletRigidBody;
    [SerializeField] float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {

      bulletRigidBody=  transform.GetComponent<Rigidbody>();
      bulletRigidBody.velocity = ShootingController.Instance.shootDir * bulletSpeed;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            Destroy(transform.gameObject);
        }

    }
}
