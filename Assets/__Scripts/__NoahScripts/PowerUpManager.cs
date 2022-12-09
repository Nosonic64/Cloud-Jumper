using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    #region private variables
    private int chanceToSpawnPowerUp;
    private AudioClip currentPlayingPowerUpClip;
    #endregion

    #region serialized variables
    [SerializeField] private GameObject[] powerUps = new GameObject[0];
    #endregion

    #region getters and setters
    public int ChanceToSpawnPowerUp { get => chanceToSpawnPowerUp; set => chanceToSpawnPowerUp = value; }
    public GameObject[] PowerUps { get => powerUps;}
    public AudioClip CurrentPlayingPowerUpClip { get => currentPlayingPowerUpClip; set => currentPlayingPowerUpClip = value; }
    #endregion
}
