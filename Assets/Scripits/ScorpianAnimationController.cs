using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpianAnimationController : MonoBehaviour
{
    Animator scorpianAnimator;
    [SerializeField]Enemy scorpian;
    void Start()
    {
        scorpianAnimator = GetComponent<Animator>();
       


    }

    // Update is called once per frame
    void Update()
    {

        scorpianAnimator.SetBool("isAttacking", scorpian.GetComponent<Enemy>().IsAttacking());
  
        scorpianAnimator.SetBool("isWalking", scorpian.GetComponent<Enemy>().IsWalking());

        
    }
}
