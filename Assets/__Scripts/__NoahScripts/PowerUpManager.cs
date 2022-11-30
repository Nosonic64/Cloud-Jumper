using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    #region private variables
    private int chanceToSpawnPowerUp;
    #endregion

    #region serialized variables
    [SerializeField] private GameObject[] powerUps = new GameObject[0];
    [SerializeField] private int chanceToSpawnPowerUpSet;
    [SerializeField] private int addToChance;
    #endregion

    #region getters and setters
    public int ChanceToSpawnPowerUp { get => chanceToSpawnPowerUp; set => chanceToSpawnPowerUp = value; }
    public GameObject[] PowerUps { get => powerUps;}
    public int AddToChance { get => addToChance;}
    public int ChanceToSpawnPowerUpSet { get => chanceToSpawnPowerUpSet;}
    #endregion

    private void Start()
    {
        chanceToSpawnPowerUp = chanceToSpawnPowerUpSet;
    }

}
