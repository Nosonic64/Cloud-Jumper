using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Base Class
public class Hazard : MonoBehaviour
{
    //[SerializeField] protected int damage;
    [SerializeField] protected float moveDistance;
    [SerializeField] protected float moveSpeed;


    public virtual void Move() {
        Debug.Log("Moving Hazard Left and Right");
    }

    

}
