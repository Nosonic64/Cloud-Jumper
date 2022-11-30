using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkManager : MonoBehaviour
{
    #region serialized variables
    [SerializeField] private GameObject[] levelChunks = new GameObject[0];
    #endregion

    #region getters and setters
    public GameObject[] LevelChunks { get => levelChunks;}
    #endregion
}
