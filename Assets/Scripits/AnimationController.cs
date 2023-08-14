using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class AnimationController : MonoBehaviour
{
    Animator playerAnimator;
    void Start()
    {
       playerAnimator= GetComponent<Animator>();
    }
    private void Update()
    {

        PlayerAnimationController();

     


       
        





    }


    private void PlayerAnimationController()
    {
        if (Player.Instance.IsWalking())
        {
            playerAnimator.SetBool("isWalking", true);
        }
        if (!Player.Instance.IsWalking())
        {
            playerAnimator.SetBool("isWalking", false);
        }
        if (ShootingController.Instance.lockUpdate)
        {
            playerAnimator.SetBool("isSingleMode", ShootingController.Instance.IsSingleMode());
        }
        if (!ShootingController.Instance.lockUpdate)
        {
            playerAnimator.SetBool("isSingleMode", ShootingController.Instance.IsSingleMode());
        }
        if (ShootingController.Instance.lockUpdate)
        {
            playerAnimator.SetBool("isBrustMode", ShootingController.Instance.IsBrustMode());
        }
        if (!ShootingController.Instance.lockUpdate)
        {
            playerAnimator.SetBool("isBrustMode", ShootingController.Instance.IsBrustMode());
        }

    }
}
