using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Base Class
public class Hazard : MonoBehaviour
{
    [SerializeField] protected float moveDistance;
    [SerializeField] protected float moveSpeed;

    public virtual void Move() 
    {
        transform.position -= new Vector3 (moveDistance, 0, 0) * Time.deltaTime * moveSpeed;
    }

    public void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }


}
