using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkManager : MonoBehaviour
{
    #region private variables
    private float resetTimer = 10f;
    private float resetTimerCounter;
    #endregion

    #region serialized variables
    [SerializeField] private GameObject[] levelChunks = new GameObject[0];
    [Header("Debug")]
    [SerializeField] private bool dontBreakPlats;
    #endregion

    #region getters and setters
    public GameObject[] LevelChunks { get => levelChunks;}
    public float ResetTimer { get => resetTimer;}
    public float ResetTimerCounter { get => resetTimerCounter; set => resetTimerCounter = value; }
    public bool DontBreakPlats { get => dontBreakPlats;}
    #endregion

    private void Update()
    {
        if(resetTimerCounter > 0)
        {
            resetTimerCounter -= Time.deltaTime;    
        }
    }
}
