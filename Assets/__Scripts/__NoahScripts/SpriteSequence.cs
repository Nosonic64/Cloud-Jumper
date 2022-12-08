using UnityEngine;

public class SpriteSequence : MonoBehaviour
{
    #region private variables
    private bool spriteCarryingPlayer;
    private Animator anim;
    #endregion

    #region getters and setters
    public bool SpriteCarryingPlayer { get => spriteCarryingPlayer; }
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartAnim()
    {
        anim.Play("Sprite_Sequence");
    }
    public void SetPlayer()
    {
        GameManager.instance.player.transform.position = new Vector3(8, 8, 0);
    }

    public void ParentPlayer()
    {
        GameManager.instance.player.transform.parent = gameObject.transform;
    }

    //Unity animation events allow you to call functions, but not if they have arguments for some reason, so i have to call 2 seperate functions to change a boolean
    public void SetSpriteCarryTrue()
    {
        spriteCarryingPlayer = true;
    }
    public void SetSpriteCarryFalse()
    {
        spriteCarryingPlayer = false;
    }

    public void UnparentPlayer()
    {
        GameManager.instance.player.transform.parent = null;
        GameManager.instance.player.ResetFromBell();
    }

    public void GoBackToIdle()
    {
        anim.Play("Sprite_Idle");
    }
}

