using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    public float dropSpeed;
 
    Vector3 direction;
    // Start is called before the first frame update
    public void Init(Vector3 dir)
    {
        direction = dir;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * dropSpeed * Time.deltaTime;
    }
}
