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
        if (GameManager.instance.player.TouchingYClamp || GameManager.instance.levelChunkManager.ResetTimerCounter > 0)
        {
            anim.speed = animSpeed;
        }
        else
        {
            anim.speed = 0;
        }
    }
}
