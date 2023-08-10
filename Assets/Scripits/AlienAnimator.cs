using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAnimator : MonoBehaviour
{
    Animator alienAnimator;
    [SerializeField] Enemy alien;
    void Start()
    {
        alienAnimator = GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {

        alienAnimator.SetBool("isAttacking", alien.GetComponent<Enemy>().IsAttacking());

        alienAnimator.SetBool("isWalking", alien.GetComponent<Enemy>().IsWalking());


    }
}
