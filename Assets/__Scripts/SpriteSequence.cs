using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSequence : MonoBehaviour
{
    public GameObject player;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetPlayer()
    {
        player.transform.position = new Vector3(0, 8, 14);
    }

    public void ParentPlayer()
    {
        player.transform.parent = gameObject.transform;
    }

    //Unity animation events allow you to call functions, but not if they have arguments for some reason, so i have to call 2 seperate functions to change a boolean
    public void SetSpriteCarryTrue()
    {
        PlayerInfo.spriteCarry = true;
    }
    public void SetSpriteCarryFalse()
    {
        PlayerInfo.hasInvulnerable = 8f;
        PlayerInfo.spriteCarry = false;
    }

    public void UnparentPlayer()
    {
        player.transform.parent = null;
        var playerScript = player.GetComponent<PlayerMovement>();
        playerScript.BellPowerNew(false, false, true);
    }

    public void GoBackToIdle()
    {
        anim.Play("Sprite_Idle");
    }
}

