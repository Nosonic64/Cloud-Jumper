using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartPlat : MonoBehaviour
{
    // This script handles despawning the starting platform
    // We do this in order to make sure the player doesnt 
    // just idle on the platform for a long time.
    #region private variables
    private float leaveSet = 5f;
    private float leaveCounter;
    #endregion

    void Update()
    {
        if (leaveCounter < leaveSet) // Once the counter hits the value in leaveSet, we destroy the object.
        {
            leaveCounter += Time.deltaTime;
        }
        else
        {
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
        }
    }
}
