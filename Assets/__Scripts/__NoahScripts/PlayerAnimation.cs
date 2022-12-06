using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour
{
    //[SerializeField] bool checkWorking;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //checkWorking = GameManager.instance.player.Grounded;
        DirectionFacing(GameManager.instance.player.LastInputDir);
        anim.SetInteger("horizontalAnim", (int)Math.Sign(GameManager.instance.player.HorizontalInput));
        anim.SetBool("grounded", GameManager.instance.player.Grounded);
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
