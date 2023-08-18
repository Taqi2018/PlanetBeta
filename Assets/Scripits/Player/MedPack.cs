using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedPack : MonoBehaviour
{

    private float rotationY;
    [SerializeField] private float rotationSpeed;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Time.deltaTime * rotationSpeed;

        transform.rotation =             Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
       // transform.RotateAround(Vector3.zero, Vector3.up, 1.0f);
        //transform.Rotate(transform.position, rotationAngle);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            HealthBooster();
            
        }
    }
    public void HealthBooster()
    {
        if (Player.Instance.health <= 90)
        {
            Player.Instance.health += 10;
         
        }
        Destroy(transform.gameObject);
    }





}
