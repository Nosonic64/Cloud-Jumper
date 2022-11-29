using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && PlayerInfo.hasInvulnerable <= 0f)
        {
            var playerScript = other.gameObject.GetComponent<PlayerMovement>();
            var myMesh = GetComponentInChildren<MeshRenderer>();
            playerScript.meshRenderer.material = myMesh.material;
            PlayerInfo.playerLives--;
            PlayerInfo.hasInvulnerable = 1f;
            Destroy(gameObject);
        }
    }
}
