using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAnim : MonoBehaviour
{
    // Controls the animation speed of the top and bottom scrolls on the playfield
    #region private variables
    private Animator anim;
    #endregion

    #region serialized fields
    [Range(0,1)]
    [SerializeField] private float animSpeed;
    #endregion

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
        else if(!GameManager.instance.player.BeforeStart) //Moves the scroll as fast as the passive scroll as long as the player has started the game.
        {
           anim.speed = GameManager.instance.levelChunkManager.PassiveScrollMultiple;
        }
        else
        {
            anim.speed = 0;
        }
    }
}
