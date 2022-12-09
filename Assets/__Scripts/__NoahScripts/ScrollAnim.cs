using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAnim : MonoBehaviour
{
    private Animator anim;

    [Range(0,1)]
    [SerializeField] private float animSpeed;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (GameManager.instance.player.TouchingYClamp || GameManager.instance.levelChunkManager.ResetTimerCounter > 0 || GameManager.instance.bellSprite.SpriteCarryingPlayer || GameManager.instance.levelChunkManager.PassiveScrollMultiple > animSpeed)
        {
            anim.speed = animSpeed;
        }
        else if(GameManager.instance.levelChunkManager.CurrentDifficulty > 0) //Moves the scroll as fast as the passive scroll is.
        {
           anim.speed = GameManager.instance.levelChunkManager.PassiveScrollMultiple;
        }
        else
        {
            anim.speed = 0;
        }
    }
}
