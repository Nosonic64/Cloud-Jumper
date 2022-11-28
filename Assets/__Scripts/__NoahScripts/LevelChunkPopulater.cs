using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkPopulater : MonoBehaviour
{
    public GameObject[] levelChunks;
    private int i;

    private void Awake()
    {
        foreach(GameObject levelChunk in levelChunks)
        {
            LevelChunkHandler.levelChunks[i] = levelChunks[i];
            i++;
        }
    }
}
