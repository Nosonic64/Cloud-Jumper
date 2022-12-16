using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpDrum : MonoBehaviour
{
    // Script on the drum that appears when players double jump.
    // Affects where it spawns and parents it to the current level chunk.
    private void Awake()
    {
        var currentLevelChunk = FindObjectOfType<LevelChunk>();
        transform.parent = currentLevelChunk.transform;
        transform.position -= new Vector3(0, 0.8f, 0);
    }
    private void DestroySelf() // When the drum hits a certain point of its animation, we call up this function to destroy it
    {
        Destroy(gameObject);
    }
}
