using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerScript = other.gameObject.GetComponent<PlayerMovement>();
            playerScript.BellPower();
            Destroy(gameObject);
        }
    }

}
