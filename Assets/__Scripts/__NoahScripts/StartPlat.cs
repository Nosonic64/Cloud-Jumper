using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlat : MonoBehaviour
{
    private float leaveSet = 5f;
    private float leaveCounter;

    void Update()
    {
        if (leaveCounter < leaveSet)
        {
            leaveCounter += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
