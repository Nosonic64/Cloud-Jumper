using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallKillBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(GameManager.instance.player.PlayerLives > 0)
            {
                GameManager.instance.player.NormalRespawn();
            }
        }
    }
}
