using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPopulater : MonoBehaviour
{
    public GameObject[] powerUpsArray;
    private int i;

    private void Awake()
    {
        foreach (GameObject powerUp in powerUpsArray)
        {
            PowerUpHandler.powerUps[i] = powerUpsArray[i];
            i++;
        }
    }
}
