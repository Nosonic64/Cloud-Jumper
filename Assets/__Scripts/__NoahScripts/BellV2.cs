using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellV2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerScript = other.gameObject.GetComponent<PlayerMovement>();
            var sprite = FindObjectOfType<SpriteSequence>();
            sprite.anim.Play("Sprite_Sequence");
            playerScript.BellPowerNew(true,true,false);
            Destroy(gameObject);
        }
    }
}
