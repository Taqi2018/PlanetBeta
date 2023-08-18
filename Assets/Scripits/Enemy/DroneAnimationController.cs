using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimationController : MonoBehaviour
{

    Animator Animator;
    [SerializeField] Enemy drone;
    void Start()
    {
        Animator = GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {

    

        Animator.SetBool("isWalking", drone.GetComponent<Enemy>().IsWalking());


        Animator.SetBool("isDieing", drone.GetComponent<Enemy>().isDroneDead);


    }
}

