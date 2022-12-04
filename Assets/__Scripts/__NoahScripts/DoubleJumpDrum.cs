using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpDrum : MonoBehaviour
{
    private void Awake()
    {
        var currentLevelChunk = FindObjectOfType<LevelChunk>();
        transform.parent = currentLevelChunk.transform;
        transform.position -= new Vector3(0, 0.8f, 0);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
