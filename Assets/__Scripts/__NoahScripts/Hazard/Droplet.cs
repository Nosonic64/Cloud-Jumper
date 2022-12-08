using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    public float dropSpeed;
 
    Vector3 direction;
    
    public void Init(Vector3 dir)
    {
        direction = dir;
        direction.Normalize();
    }

    void Update()
    {
        transform.position += direction * dropSpeed * Time.deltaTime;
    }
}
