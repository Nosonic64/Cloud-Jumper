using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    public Transform spawnPos; //empty object above camera view
    public float scrollSpeed;   // how fast background moves

    //when background no longer visable, teleport above camera
    private void OnBecameInvisible()
    {
        transform.position = spawnPos.position; // once more background are in. create list or array in a background controller. turns off background then picks random from list/array
    }

    //MOVE BACKGROUND DOWN 
    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime, Space.World);
    }
}
