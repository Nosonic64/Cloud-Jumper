using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInfo.hasInvulnerable = 5f;
            var musicHandler = FindObjectOfType<MusicHandler>();
            if (musicHandler != null)
            {
                musicHandler.PlayPowerUpMusic(0);
                musicHandler.MusicFadeOutCall();
            }
            var playerScript = other.gameObject.GetComponent<PlayerMovement>();
            var myMesh = GetComponent<MeshRenderer>();
            playerScript.meshRenderer.material = myMesh.material;
            Destroy(gameObject);
        }
    }

}
