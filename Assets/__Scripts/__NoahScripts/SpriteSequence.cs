using UnityEngine;

public class SpriteSequence : MonoBehaviour
{
    // This script controls the thing that carries the player up after
    // collecting a bell.
    // This script is entirely functions that get called from a Unity Animation
    // using its events system.
    // https://docs.unity3d.com/Manual/script-AnimationWindowEvent.html
    // We do this because its an easy way to do a sequence of events that
    // we know is gonna be the same every time.
    #region private variables
    private bool spriteCarryingPlayer;
    private Animator anim;
    #endregion

    #region getters and setters
    public bool SpriteCarryingPlayer { get => spriteCarryingPlayer; }
    public Animator Anim { get => anim; set => anim = value; }
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartSpriteSequence() //Starts the animation that has all the event markers
    {
        anim.Play("Sprite_Sequence");
    }
    public void SetPlayer()
    {
        GameManager.instance.player.transform.position = new Vector3(8, 8, 0);
    }

    public void ParentPlayer(int i)
    {
        switch (i)
        {
            case 0:
            GameManager.instance.player.transform.parent = gameObject.transform;
                break;
            case 1:
                GameManager.instance.player.transform.parent = null;
                GameManager.instance.player.ResetFromBell();
                break;

        }
    }

    public void SetSpriteCarry(int i)
    {
        switch (i)
        {
            case 0:
                spriteCarryingPlayer = true;
                break;
            case 1:
                spriteCarryingPlayer = false;
                break;
        }
    }

    private void StopSpriteSequence()
    {
        anim.Play("Sprite_Idle");
    }
}

