using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private float animLastInputDir;
    private float animHorizontal;

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

    private void DirectionFacing(float lastInputdir)
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
