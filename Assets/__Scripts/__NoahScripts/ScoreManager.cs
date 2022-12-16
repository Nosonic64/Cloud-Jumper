using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Script that holds the players score (distance) and their 
    // current top score for a play session.
    #region private variables
    private float distance;
    private float currentPlayerTopDistance;
    #endregion

    #region getters and setters
    public float Distance { get => distance; set => distance = value; }
    public float CurrentPlayerTopDistance { get => currentPlayerTopDistance; set => currentPlayerTopDistance = value; }
    #endregion
}
