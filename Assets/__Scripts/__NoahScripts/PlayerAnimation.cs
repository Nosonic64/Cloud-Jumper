using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour
{
    // This script handles player animation.
    // This is seperate from PlayerMovement to keep things tidier.
    // Script heavily uses Unitys inbuilt Animator component and systems.

    #region private variables
    private Animator anim;
    private float animLastInputDir;
    private float animHorizontal;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        animHorizontal = Input.GetAxisRaw("Horizontal");

        if (animHorizontal != 0)
        {
            animLastInputDir = animHorizontal;
        }

        DirectionFacing(animLastInputDir);
        anim.SetInteger("horizontalAnim", Math.Sign(animHorizontal));
        anim.SetBool("grounded", GameManager.instance.player.Grounded);
        anim.SetFloat("playerVelocityY", GameManager.instance.player.GetRigidbody.velocity.y);
    }

    private void DirectionFacing(float lastInputdir) //Changes the facing direction of the fox mesh dependent on what direction the player last pressed.
    {
        if(lastInputdir < 0)
        {
            transform.eulerAngles = new Vector3(0, 270);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90);
        }
    }
}
